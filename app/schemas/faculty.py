from pydantic import BaseModel
from uuid import UUID
from typing import Optional


class FacultyBase(BaseModel):
    name: str
    short_name: str


class FacultyCreate(FacultyBase):
    pass


class FacultyUpdate(BaseModel):
    name: Optional[str] = None
    short_name: Optional[str] = None


class FacultyRead(FacultyBase):
    id: UUID

    class Config:
        from_attributes = True
