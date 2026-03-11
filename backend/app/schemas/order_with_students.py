from pydantic import BaseModel
from uuid import UUID
from datetime import date
from typing import List, Optional


class StudentForEnrollment(BaseModel):
    """Студент для зачисления"""
    name: str
    last_name: str
    patronymic: Optional[str] = None
    study_book_number: int
    group_id: UUID


class EnrollmentOrderWithStudentsCreate(BaseModel):
    """Приказ о зачислении со студентами"""
    order: dict
    enrollment_order: dict
    students: List[StudentForEnrollment]


class StudentsForOrderCreate(BaseModel):
    """Список студентов для приказа"""
    student_ids: List[UUID]


class ExpulsionOrderWithStudentsCreate(BaseModel):
    """Приказ об отчислении"""
    order: dict
    expulsion_order: dict
    student_ids: List[UUID]


class AcademicLeaveOrderWithStudentsCreate(BaseModel):
    """Приказ об академическом отпуске"""
    order: dict
    academic_leave_order: dict
    student_ids: List[UUID]


class NextCourseOrderWithStudentsCreate(BaseModel):
    """Приказ о переводе на следующий курс"""
    order: dict
    next_course_order: dict
    student_ids: List[UUID]


class TransferDirectionOrderWithStudentsCreate(BaseModel):
    """Приказ о переводе на другое направление"""
    order: dict
    transfer_direction_order: dict
    student_ids: List[UUID]
