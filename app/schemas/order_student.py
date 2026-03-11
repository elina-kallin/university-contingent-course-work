from pydantic import BaseModel
from uuid import UUID


class OrderStudentBase(BaseModel):
    pass


class OrderStudentCreate(BaseModel):
    order_id: UUID
    student_id: UUID


class OrderStudentRead(BaseModel):
    id: UUID
    order_id: UUID
    student_id: UUID

    class Config:
        from_attributes = True
