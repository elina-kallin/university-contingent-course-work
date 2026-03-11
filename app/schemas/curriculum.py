from pydantic import BaseModel
from uuid import UUID


class CurriculumBase(BaseModel):
    pass


class CurriculumCreate(BaseModel):
    direction_id: UUID


class CurriculumUpdate(BaseModel):
    direction_id: UUID


class CurriculumRead(BaseModel):
    id: UUID
    direction_id: UUID

    class Config:
        from_attributes = True
