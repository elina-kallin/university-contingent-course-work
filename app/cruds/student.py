from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.student import Student
from app.schemas.student import StudentCreate, StudentUpdate


def get_students(db: Session, skip: int = 0, limit: int = 100) -> List[Student]:
    return db.query(Student).offset(skip).limit(limit).all()


def get_student(db: Session, student_id: UUID) -> Optional[Student]:
    return db.query(Student).filter(Student.id == student_id).first()


def create_student(db: Session, student: StudentCreate) -> Student:
    db_student = Student(**student.model_dump())
    db.add(db_student)
    db.commit()
    db.refresh(db_student)
    return db_student


def update_student(db: Session, student_id: UUID, student: StudentUpdate) -> Optional[Student]:
    db_student = get_student(db, student_id)
    if db_student:
        update_data = student.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_student, field, value)
        db.commit()
        db.refresh(db_student)
    return db_student


def delete_student(db: Session, student_id: UUID) -> Optional[Student]:
    db_student = get_student(db, student_id)
    if db_student:
        db.delete(db_student)
        db.commit()
    return db_student


def get_students_by_group(db: Session, group_id: UUID, skip: int = 0, limit: int = 100) -> List[Student]:
    return db.query(Student).filter(Student.group_id == group_id).offset(skip).limit(limit).all()


def search_students(
    db: Session,
    search_query: Optional[str] = None,
    group_id: Optional[UUID] = None,
    status: Optional[str] = None,
    skip: int = 0,
    limit: int = 100
) -> List[Student]:
    query = db.query(Student)

    if search_query:
        query = query.filter(
            (Student.name.ilike(f"%{search_query}%")) |
            (Student.last_name.ilike(f"%{search_query}%")) |
            (Student.patronymic.ilike(f"%{search_query}%"))
        )

    if group_id:
        query = query.filter(Student.group_id == group_id)

    if status:
        query = query.filter(Student.status == status)

    return query.offset(skip).limit(limit).all()
