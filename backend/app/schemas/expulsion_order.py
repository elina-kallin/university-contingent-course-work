from pydantic import BaseModel
from uuid import UUID
from datetime import date
from typing import Optional
from app.enums import ExpulsionReason


class ExpulsionOrderBase(BaseModel):
    expulsion_date: Optional[date] = None
    expulsion_reason: Optional[ExpulsionReason] = None


class ExpulsionOrderCreate(BaseModel):
    order_id: UUID
    expulsion_date: Optional[date] = None
    expulsion_reason: Optional[ExpulsionReason] = None


class ExpulsionOrderUpdate(BaseModel):
    expulsion_date: Optional[date] = None
    expulsion_reason: Optional[ExpulsionReason] = None


class ExpulsionOrderRead(BaseModel):
    order_id: UUID
    expulsion_date: Optional[date] = None
    expulsion_reason: Optional[ExpulsionReason] = None

    class Config:
        from_attributes = True
