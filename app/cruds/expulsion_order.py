from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.expulsion_order import ExpulsionOrder
from app.schemas.expulsion_order import ExpulsionOrderCreate, ExpulsionOrderUpdate


def get_expulsion_orders(db: Session, skip: int = 0, limit: int = 100) -> List[ExpulsionOrder]:
    return db.query(ExpulsionOrder).offset(skip).limit(limit).all()


def get_expulsion_order(db: Session, order_id: UUID) -> Optional[ExpulsionOrder]:
    return db.query(ExpulsionOrder).filter(ExpulsionOrder.order_id == order_id).first()


def create_expulsion_order(db: Session, expulsion_order: ExpulsionOrderCreate) -> ExpulsionOrder:
    db_expulsion_order = ExpulsionOrder(**expulsion_order.model_dump())
    db.add(db_expulsion_order)
    db.commit()
    db.refresh(db_expulsion_order)
    return db_expulsion_order


def update_expulsion_order(db: Session, order_id: UUID, expulsion_order: ExpulsionOrderUpdate) -> Optional[ExpulsionOrder]:
    db_expulsion_order = get_expulsion_order(db, order_id)
    if db_expulsion_order:
        update_data = expulsion_order.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_expulsion_order, field, value)
        db.commit()
        db.refresh(db_expulsion_order)
    return db_expulsion_order


def delete_expulsion_order(db: Session, order_id: UUID) -> Optional[ExpulsionOrder]:
    db_expulsion_order = get_expulsion_order(db, order_id)
    if db_expulsion_order:
        db.delete(db_expulsion_order)
        db.commit()
    return db_expulsion_order
