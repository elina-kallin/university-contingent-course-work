from pydantic import BaseModel
from uuid import UUID
from typing import Optional


class GroupBase(BaseModel):
    name: str
    course: int


class GroupCreate(BaseModel):
    name: str
    course: int
    direction_id: UUID


class GroupUpdate(BaseModel):
    name: Optional[str] = None
    course: Optional[int] = None
    direction_id: Optional[UUID] = None


class GroupRead(BaseModel):
    id: UUID
    name: str
    course: int
    direction_id: UUID

    class Config:
        from_attributes = True
