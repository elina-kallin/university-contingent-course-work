from pydantic import BaseModel
from uuid import UUID
from typing import Optional


class EnrollmentOrderBase(BaseModel):
    education_form: str
    price: Optional[str] = None


class EnrollmentOrderCreate(BaseModel):
    order_id: UUID
    education_form: str
    price: Optional[str] = None


class EnrollmentOrderUpdate(BaseModel):
    education_form: Optional[str] = None
    price: Optional[str] = None


class EnrollmentOrderRead(BaseModel):
    order_id: UUID
    education_form: str
    price: Optional[str] = None

    class Config:
        from_attributes = True
