from pydantic import BaseModel
from uuid import UUID
from datetime import date
from typing import Optional
from app.enums import AcademicLeaveReason


class AcademicLeaveOrderBase(BaseModel):
    leave_start: Optional[date] = None
    leave_end: Optional[date] = None
    leave_reason: Optional[AcademicLeaveReason] = None


class AcademicLeaveOrderCreate(BaseModel):
    order_id: UUID
    leave_start: Optional[date] = None
    leave_end: Optional[date] = None
    leave_reason: Optional[AcademicLeaveReason] = None


class AcademicLeaveOrderUpdate(BaseModel):
    leave_start: Optional[date] = None
    leave_end: Optional[date] = None
    leave_reason: Optional[AcademicLeaveReason] = None


class AcademicLeaveOrderRead(BaseModel):
    order_id: UUID
    leave_start: Optional[date] = None
    leave_end: Optional[date] = None
    leave_reason: Optional[AcademicLeaveReason] = None

    class Config:
        from_attributes = True
