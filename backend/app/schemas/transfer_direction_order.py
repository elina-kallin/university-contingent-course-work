from pydantic import BaseModel
from uuid import UUID
from typing import Optional


class TransferDirectionOrderBase(BaseModel):
    from_direction_id: Optional[UUID] = None
    to_direction_id: Optional[UUID] = None


class TransferDirectionOrderCreate(BaseModel):
    order_id: UUID
    from_direction_id: Optional[UUID] = None
    to_direction_id: Optional[UUID] = None


class TransferDirectionOrderUpdate(BaseModel):
    from_direction_id: Optional[UUID] = None
    to_direction_id: Optional[UUID] = None


class TransferDirectionOrderRead(BaseModel):
    order_id: UUID
    from_direction_id: Optional[UUID] = None
    to_direction_id: Optional[UUID] = None

    class Config:
        from_attributes = True
