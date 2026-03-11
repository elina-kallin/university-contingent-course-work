from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.transfer_direction_order import TransferDirectionOrder
from app.schemas.transfer_direction_order import TransferDirectionOrderCreate, TransferDirectionOrderUpdate


def get_transfer_direction_orders(db: Session, skip: int = 0, limit: int = 100) -> List[TransferDirectionOrder]:
    return db.query(TransferDirectionOrder).offset(skip).limit(limit).all()


def get_transfer_direction_order(db: Session, order_id: UUID) -> Optional[TransferDirectionOrder]:
    return db.query(TransferDirectionOrder).filter(TransferDirectionOrder.order_id == order_id).first()


def create_transfer_direction_order(db: Session, transfer_direction_order: TransferDirectionOrderCreate) -> TransferDirectionOrder:
    db_transfer_direction_order = TransferDirectionOrder(**transfer_direction_order.model_dump())
    db.add(db_transfer_direction_order)
    db.commit()
    db.refresh(db_transfer_direction_order)
    return db_transfer_direction_order


def update_transfer_direction_order(db: Session, order_id: UUID, transfer_direction_order: TransferDirectionOrderUpdate) -> Optional[TransferDirectionOrder]:
    db_transfer_direction_order = get_transfer_direction_order(db, order_id)
    if db_transfer_direction_order:
        update_data = transfer_direction_order.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_transfer_direction_order, field, value)
        db.commit()
        db.refresh(db_transfer_direction_order)
    return db_transfer_direction_order


def delete_transfer_direction_order(db: Session, order_id: UUID) -> Optional[TransferDirectionOrder]:
    db_transfer_direction_order = get_transfer_direction_order(db, order_id)
    if db_transfer_direction_order:
        db.delete(db_transfer_direction_order)
        db.commit()
    return db_transfer_direction_order
