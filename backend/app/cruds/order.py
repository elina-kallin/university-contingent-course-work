from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.order import Order
from app.schemas.order import OrderCreate, OrderUpdate


def get_orders(db: Session, skip: int = 0, limit: int = 100) -> List[Order]:
    return db.query(Order).offset(skip).limit(limit).all()


def get_order(db: Session, order_id: UUID) -> Optional[Order]:
    return db.query(Order).filter(Order.id == order_id).first()


def create_order(db: Session, order: OrderCreate) -> Order:
    db_order = Order(**order.model_dump())
    db.add(db_order)
    db.commit()
    db.refresh(db_order)
    return db_order


def update_order(db: Session, order_id: UUID, order: OrderUpdate) -> Optional[Order]:
    db_order = get_order(db, order_id)
    if db_order:
        update_data = order.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_order, field, value)
        db.commit()
        db.refresh(db_order)
    return db_order


def delete_order(db: Session, order_id: UUID) -> Optional[Order]:
    db_order = get_order(db, order_id)
    if db_order:
        db.delete(db_order)
        db.commit()
    return db_order


def get_orders_by_type(db: Session, order_type: str, skip: int = 0, limit: int = 100) -> List[Order]:
    return db.query(Order).filter(Order.type == order_type).offset(skip).limit(limit).all()
