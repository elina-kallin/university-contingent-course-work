from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.next_course_order import NextCourseOrder
from app.schemas.next_course_order import NextCourseOrderCreate, NextCourseOrderUpdate


def get_next_course_orders(db: Session, skip: int = 0, limit: int = 100) -> List[NextCourseOrder]:
    return db.query(NextCourseOrder).offset(skip).limit(limit).all()


def get_next_course_order(db: Session, order_id: UUID) -> Optional[NextCourseOrder]:
    return db.query(NextCourseOrder).filter(NextCourseOrder.order_id == order_id).first()


def create_next_course_order(db: Session, next_course_order: NextCourseOrderCreate) -> NextCourseOrder:
    db_next_course_order = NextCourseOrder(**next_course_order.model_dump())
    db.add(db_next_course_order)
    db.commit()
    db.refresh(db_next_course_order)
    return db_next_course_order


def update_next_course_order(db: Session, order_id: UUID, next_course_order: NextCourseOrderUpdate) -> Optional[NextCourseOrder]:
    db_next_course_order = get_next_course_order(db, order_id)
    if db_next_course_order:
        update_data = next_course_order.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_next_course_order, field, value)
        db.commit()
        db.refresh(db_next_course_order)
    return db_next_course_order


def delete_next_course_order(db: Session, order_id: UUID) -> Optional[NextCourseOrder]:
    db_next_course_order = get_next_course_order(db, order_id)
    if db_next_course_order:
        db.delete(db_next_course_order)
        db.commit()
    return db_next_course_order
