from fastapi import APIRouter, Depends
from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional
from datetime import date
from pydantic import BaseModel

from app.database.database import get_db
from app.core.dependencies import get_current_dean
from app.models.dean import Dean
from app.models.student import Student
from app.models.group import Group
from app.models.direction import Direction
from app.models.faculty import Faculty

router = APIRouter(prefix="/reports", tags=["Reports"])


class ContingentReportItem(BaseModel):
    id: UUID
    name: str
    status: str
    count: int

    class Config:
        from_attributes = True


class ContingentReport(BaseModel):
    period_start: date
    period_end: date
    filter_type: str  # faculty, direction, group
    filter_id: UUID
    data: List[ContingentReportItem]


@router.get("/contingent", response_model=ContingentReport)
def get_contingent_report(
    period_start: date,
    period_end: date,
    faculty_id: Optional[UUID] = None,
    direction_id: Optional[UUID] = None,
    group_id: Optional[UUID] = None,
    db: Session = Depends(get_db),
    dean: Dean = Depends(get_current_dean)
):
    """
    Отчёт по контингенту за период.
    Можно фильтровать по факультету, направлению или группе.
    Возвращает количество студентов по статусам.
    """
    query = db.query(Student).join(Group)
    
    # Определяем тип фильтра
    filter_type = "all"
    filter_id = None
    
    if group_id:
        query = query.filter(Student.group_id == group_id)
        filter_type = "group"
        filter_id = group_id
    elif direction_id:
        query = query.join(Direction).filter(Direction.id == direction_id)
        filter_type = "direction"
        filter_id = direction_id
    elif faculty_id:
        query = query.join(Direction).join(Faculty).filter(Faculty.id == faculty_id)
        filter_type = "faculty"
        filter_id = faculty_id
    else:
        # Если ничего не выбрано, фильтруем по факультету декана
        if dean.faculty_id:
            query = query.join(Direction).join(Faculty).filter(Faculty.id == dean.faculty_id)
            filter_type = "faculty"
            filter_id = dean.faculty_id
    
    # Фильтр по периоду (студенты, которые были активны в этот период)
    query = query.filter(
        (Student.enrollment_date <= period_end) &
        ((Student.expulsion_date == None) | (Student.expulsion_date >= period_start))
    )
    
    students = query.all()
    
    # Группировка по статусам
    status_counts = {}
    for student in students:
        status = student.status
        if status not in status_counts:
            status_counts[status] = []
        status_counts[status].append(student)
    
    # Формирование ответа
    report_data = []
    for status, status_students in status_counts.items():
        report_data.append(ContingentReportItem(
            id=status_students[0].id if status_students else UUID(int=0),
            name=status,
            status=status,
            count=len(status_students)
        ))
    
    return ContingentReport(
        period_start=period_start,
        period_end=period_end,
        filter_type=filter_type,
        filter_id=filter_id,
        data=report_data
    )


@router.get("/contingent/detailed")
def get_contingent_report_detailed(
    period_start: date,
    period_end: date,
    faculty_id: Optional[UUID] = None,
    direction_id: Optional[UUID] = None,
    group_id: Optional[UUID] = None,
    db: Session = Depends(get_db),
    dean: Dean = Depends(get_current_dean)
):
    """
    Детальный отчёт по контингенту - список всех студентов с группами.
    """
    query = db.query(Student).join(Group).join(Direction)
    
    if group_id:
        query = query.filter(Student.group_id == group_id)
    elif direction_id:
        query = query.filter(Direction.id == direction_id)
    elif faculty_id:
        query = query.join(Faculty).filter(Faculty.id == faculty_id)
    else:
        if dean.faculty_id:
            query = query.join(Faculty).filter(Faculty.id == dean.faculty_id)
    
    # Фильтр по периоду
    query = query.filter(
        (Student.enrollment_date <= period_end) &
        ((Student.expulsion_date == None) | (Student.expulsion_date >= period_start))
    )
    
    students = query.all()
    
    return {
        "period_start": period_start,
        "period_end": period_end,
        "total_count": len(students),
        "students": [
            {
                "id": s.id,
                "full_name": f"{s.last_name} {s.name} {s.patronymic or ''}",
                "study_book_number": s.study_book_number,
                "status": s.status,
                "group": s.group.name,
                "course": s.group.course,
                "direction": s.group.direction.name
            }
            for s in students
        ]
    }
