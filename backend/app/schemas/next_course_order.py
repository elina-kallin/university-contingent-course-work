from pydantic import BaseModel
from uuid import UUID
from typing import Optional


class NextCourseOrderBase(BaseModel):
    from_course: Optional[int] = None
    to_course: Optional[int] = None


class NextCourseOrderCreate(BaseModel):
    order_id: UUID
    from_course: Optional[int] = None
    to_course: Optional[int] = None


class NextCourseOrderUpdate(BaseModel):
    from_course: Optional[int] = None
    to_course: Optional[int] = None


class NextCourseOrderRead(BaseModel):
    order_id: UUID
    from_course: Optional[int] = None
    to_course: Optional[int] = None

    class Config:
        from_attributes = True
