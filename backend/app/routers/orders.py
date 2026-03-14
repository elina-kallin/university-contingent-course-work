from fastapi import APIRouter, Depends, HTTPException, Response
from sqlalchemy.orm import Session
from uuid import UUID
from typing import List

from app.database.database import get_db
from app.cruds import order as crud
from app.cruds import order_student as order_student_crud
from app.services.print_service import generate_enrollment_order_html, generate_expulsion_order_html, generate_generic_order_html
from app.schemas.order import OrderCreate, OrderUpdate, OrderRead
from app.schemas.order_student import OrderStudentCreate, OrderStudentRead
from app.core.dependencies import get_current_dean
from app.models.dean import Dean
from app.enums import OrderType

router = APIRouter(prefix="/orders", tags=["Orders"])


@router.get("/", response_model=List[OrderRead])
def get_orders(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.get_orders(db, skip, limit)


@router.get("/{order_id}", response_model=OrderRead)
def get_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = crud.get_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="Order not found")
    return db_order


@router.post("/", response_model=OrderRead)
def create_order(order: OrderCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.create_order(db, order)


@router.put("/{order_id}", response_model=OrderRead)
def update_order(order_id: UUID, order: OrderUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = crud.update_order(db, order_id, order)
    if not db_order:
        raise HTTPException(status_code=404, detail="Order not found")
    return db_order


@router.delete("/{order_id}")
def delete_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = crud.delete_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="Order not found")
    return {"message": "Order deleted"}


@router.get("/type/{order_type}", response_model=List[OrderRead])
def get_orders_by_type(order_type: str, skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.get_orders_by_type(db, order_type, skip, limit)


# OrderStudent endpoints
@router.post("/students", response_model=OrderStudentRead)
def create_order_student(order_student: OrderStudentCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return order_student_crud.create_order_student(db, order_student)


@router.delete("/students/{order_student_id}")
def delete_order_student(order_student_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order_student = order_student_crud.delete_order_student(db, order_student_id)
    if not db_order_student:
        raise HTTPException(status_code=404, detail="OrderStudent not found")
    return {"message": "OrderStudent deleted"}


@router.get("/{order_id}/students", response_model=List[OrderStudentRead])
def get_order_students(order_id: UUID, skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return order_student_crud.get_order_students_by_order(db, order_id, skip, limit)


# ============================================
# ЭНДПОИНТЫ ДЛЯ ПЕЧАТИ ПРИКАЗОВ
# ============================================

@router.get("/{order_id}/print")
def print_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    """
    Получить HTML для печати приказа.
    Откройте в браузере и нажмите Ctrl+P для печати.
    """
    from app.models.order import Order
    from app.models.student import Student
    from app.models.order_student import OrderStudent
    from app.models.group import Group
    from app.models.direction import Direction
    from app.models.faculty import Faculty

    # Получаем приказ
    order = db.query(Order).filter(Order.id == order_id).first()
    if not order:
        raise HTTPException(status_code=404, detail="Order not found")

    # Получаем студентов в приказе
    order_students = db.query(OrderStudent).filter(OrderStudent.order_id == order_id).all()
    student_ids = [os.student_id for os in order_students]
    students = db.query(Student).join(Group).join(Direction).join(Faculty).filter(
        Student.id.in_(student_ids)
    ).all()

    students_data = [
        {
            "last_name": s.last_name,
            "name": s.name,
            "patronymic": s.patronymic,
            "study_book_number": s.study_book_number,
            "group_name": s.group.name,
            "faculty_name": s.group.direction.faculty.name,
            "direction_name": s.group.direction.name,
            "education_form": "full-time",  # Можно добавить поле в Student
            "price": ""  # Можно получить из EnrollmentOrder
        }
        for s in students
    ]

    order_data = {
        "number": order.number,
        "date": str(order.date),
        "reason": order.reason,
        "type": order.type.value if hasattr(order.type, 'value') else order.type
    }
    
    # Получаем данные декана и факультета
    dean_name = dean.full_name if dean else ""
    faculty_name = dean.faculty.name if dean and dean.faculty else ""
    faculty_short_name = dean.faculty.short_name if dean and dean.faculty else ""

    # Генерируем HTML в зависимости от типа приказа
    if order.type == OrderType.ENROLLMENT:
        html = generate_enrollment_order_html(
            order_data, 
            students_data,
            university_name="Университет",
            dean_full_name=dean_name,
            faculty_name=faculty_name,
            faculty_short_name=faculty_short_name
        )
    elif order.type == OrderType.EXPULSION:
        html = generate_expulsion_order_html(
            order_data, 
            students_data,
            university_name="Университет",
            dean_full_name=dean_name
        )
    else:
        title = f"ПРИКАЗ ({order.type})"
        html = generate_generic_order_html(order_data, students_data, title)
    
    return Response(content=html, media_type="text/html")
