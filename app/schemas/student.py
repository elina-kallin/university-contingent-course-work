from pydantic import BaseModel
from uuid import UUID
from datetime import date
from typing import Optional
from app.enums import StudentStatus


class StudentBase(BaseModel):
    name: str
    last_name: str
    patronymic: Optional[str] = None
    study_book_number: int
    status: StudentStatus


class StudentCreate(BaseModel):
    name: str
    last_name: str
    patronymic: Optional[str] = None
    study_book_number: int
    status: StudentStatus
    group_id: UUID


class StudentUpdate(BaseModel):
    name: Optional[str] = None
    last_name: Optional[str] = None
    patronymic: Optional[str] = None
    study_book_number: Optional[int] = None
    enrollment_date: Optional[date] = None
    expulsion_date: Optional[date] = None
    status: Optional[StudentStatus] = None
    group_id: Optional[UUID] = None


class StudentRead(BaseModel):
    id: UUID
    name: str
    last_name: str
    patronymic: Optional[str] = None
    study_book_number: int
    enrollment_date: Optional[date] = None
    expulsion_date: Optional[date] = None
    status: StudentStatus
    group_id: UUID

    class Config:
        from_attributes = True
