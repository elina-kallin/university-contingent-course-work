from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.order_student import OrderStudent
from app.schemas.order_student import OrderStudentCreate


def get_order_students(db: Session, skip: int = 0, limit: int = 100) -> List[OrderStudent]:
    return db.query(OrderStudent).offset(skip).limit(limit).all()


def get_order_student(db: Session, order_student_id: UUID) -> Optional[OrderStudent]:
    return db.query(OrderStudent).filter(OrderStudent.id == order_student_id).first()


def create_order_student(db: Session, order_student: OrderStudentCreate) -> OrderStudent:
    db_order_student = OrderStudent(**order_student.model_dump())
    db.add(db_order_student)
    db.commit()
    db.refresh(db_order_student)
    return db_order_student


def delete_order_student(db: Session, order_student_id: UUID) -> Optional[OrderStudent]:
    db_order_student = get_order_student(db, order_student_id)
    if db_order_student:
        db.delete(db_order_student)
        db.commit()
    return db_order_student


def get_order_students_by_order(db: Session, order_id: UUID, skip: int = 0, limit: int = 100) -> List[OrderStudent]:
    return db.query(OrderStudent).filter(OrderStudent.order_id == order_id).offset(skip).limit(limit).all()


def get_order_students_by_student(db: Session, student_id: UUID, skip: int = 0, limit: int = 100) -> List[OrderStudent]:
    return db.query(OrderStudent).filter(OrderStudent.student_id == student_id).offset(skip).limit(limit).all()
