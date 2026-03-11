from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.academic_leave_order import AcademicLeaveOrder
from app.schemas.academic_leave_order import AcademicLeaveOrderCreate, AcademicLeaveOrderUpdate


def get_academic_leave_orders(db: Session, skip: int = 0, limit: int = 100) -> List[AcademicLeaveOrder]:
    return db.query(AcademicLeaveOrder).offset(skip).limit(limit).all()


def get_academic_leave_order(db: Session, order_id: UUID) -> Optional[AcademicLeaveOrder]:
    return db.query(AcademicLeaveOrder).filter(AcademicLeaveOrder.order_id == order_id).first()


def create_academic_leave_order(db: Session, academic_leave_order: AcademicLeaveOrderCreate) -> AcademicLeaveOrder:
    db_academic_leave_order = AcademicLeaveOrder(**academic_leave_order.model_dump())
    db.add(db_academic_leave_order)
    db.commit()
    db.refresh(db_academic_leave_order)
    return db_academic_leave_order


def update_academic_leave_order(db: Session, order_id: UUID, academic_leave_order: AcademicLeaveOrderUpdate) -> Optional[AcademicLeaveOrder]:
    db_academic_leave_order = get_academic_leave_order(db, order_id)
    if db_academic_leave_order:
        update_data = academic_leave_order.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_academic_leave_order, field, value)
        db.commit()
        db.refresh(db_academic_leave_order)
    return db_academic_leave_order


def delete_academic_leave_order(db: Session, order_id: UUID) -> Optional[AcademicLeaveOrder]:
    db_academic_leave_order = get_academic_leave_order(db, order_id)
    if db_academic_leave_order:
        db.delete(db_academic_leave_order)
        db.commit()
    return db_academic_leave_order
