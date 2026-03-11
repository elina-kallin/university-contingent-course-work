from pydantic import BaseModel
from uuid import UUID
from datetime import date
from typing import Optional
from app.enums import OrderType


class OrderBase(BaseModel):
    number: str
    date: date
    type: OrderType
    reason: str


class OrderCreate(BaseModel):
    number: str
    date: date
    type: OrderType
    reason: str


class OrderUpdate(BaseModel):
    number: Optional[str] = None
    date: Optional[date] = None
    type: Optional[OrderType] = None
    reason: Optional[str] = None


class OrderRead(BaseModel):
    id: UUID
    number: str
    date: date
    type: OrderType
    reason: str

    class Config:
        from_attributes = True
