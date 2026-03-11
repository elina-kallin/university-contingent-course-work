from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.enrollment_order import EnrollmentOrder
from app.schemas.enrollment_order import EnrollmentOrderCreate, EnrollmentOrderUpdate


def get_enrollment_orders(db: Session, skip: int = 0, limit: int = 100) -> List[EnrollmentOrder]:
    return db.query(EnrollmentOrder).offset(skip).limit(limit).all()


def get_enrollment_order(db: Session, order_id: UUID) -> Optional[EnrollmentOrder]:
    return db.query(EnrollmentOrder).filter(EnrollmentOrder.order_id == order_id).first()


def create_enrollment_order(db: Session, enrollment_order: EnrollmentOrderCreate) -> EnrollmentOrder:
    db_enrollment_order = EnrollmentOrder(**enrollment_order.model_dump())
    db.add(db_enrollment_order)
    db.commit()
    db.refresh(db_enrollment_order)
    return db_enrollment_order


def update_enrollment_order(db: Session, order_id: UUID, enrollment_order: EnrollmentOrderUpdate) -> Optional[EnrollmentOrder]:
    db_enrollment_order = get_enrollment_order(db, order_id)
    if db_enrollment_order:
        update_data = enrollment_order.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_enrollment_order, field, value)
        db.commit()
        db.refresh(db_enrollment_order)
    return db_enrollment_order


def delete_enrollment_order(db: Session, order_id: UUID) -> Optional[EnrollmentOrder]:
    db_enrollment_order = get_enrollment_order(db, order_id)
    if db_enrollment_order:
        db.delete(db_enrollment_order)
        db.commit()
    return db_enrollment_order
