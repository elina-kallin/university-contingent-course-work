from pydantic import BaseModel
from uuid import UUID
from typing import Optional
from app.enums import ControlType


class DisciplineBase(BaseModel):
    name: str
    semester: Optional[int] = None
    hours: Optional[int] = None
    control_type: ControlType


class DisciplineCreate(BaseModel):
    name: str
    semester: Optional[int] = None
    hours: Optional[int] = None
    control_type: ControlType
    curriculum_id: UUID


class DisciplineUpdate(BaseModel):
    name: Optional[str] = None
    semester: Optional[int] = None
    hours: Optional[int] = None
    control_type: Optional[ControlType] = None


class DisciplineRead(BaseModel):
    id: UUID
    name: str
    semester: Optional[int] = None
    hours: Optional[int] = None
    control_type: ControlType
    curriculum_id: UUID

    class Config:
        from_attributes = True
