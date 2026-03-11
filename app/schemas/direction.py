from pydantic import BaseModel
from uuid import UUID
from typing import Optional


class DirectionBase(BaseModel):
    name: str
    code: str
    study_duration_years: int


class DirectionCreate(BaseModel):
    name: str
    code: str
    study_duration_years: int
    faculty_id: UUID


class DirectionUpdate(BaseModel):
    name: Optional[str] = None
    code: Optional[str] = None
    study_duration_years: Optional[int] = None
    faculty_id: Optional[UUID] = None


class DirectionRead(BaseModel):
    id: UUID
    name: str
    code: str
    study_duration_years: int
    faculty_id: UUID

    class Config:
        from_attributes = True
