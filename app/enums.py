"""
Перечисления (enums) для всего приложения.
"""
from enum import Enum


class StudentStatus(str, Enum):
    """Статус студента"""
    STUDY = "study"
    EXPELLED = "expelled"
    ACADEMIC_LEAVE = "academic_leave"


class OrderType(str, Enum):
    """Тип приказа"""
    ENROLLMENT = "enrollment"
    EXPULSION = "expulsion"
    NEXT_COURSE = "next_course"
    ACADEMIC_LEAVE = "academic_leave"
    TRANSFER_DIRECTION = "transfer_direction"


class ExpulsionReason(str, Enum):
    """Причина отчисления"""
    DEBTS = "debts"
    HEALTH = "health"
    PERSONAL_REASON = "personal_reason"
    OWN = "own"


class AcademicLeaveReason(str, Enum):
    """Причина академического отпуска"""
    HEALTH = "health"
    PERSONAL_FAMILY = "personal/family"


class ControlType(str, Enum):
    """Тип контроля дисциплины"""
    TEST = "test"
    EXAM = "exam"
    COURSE_WORK = "course work"


class EducationForm(str, Enum):
    """Форма обучения"""
    FULL_TIME = "full-time"
    PART_TIME = "part-time"
    EXTRAMURAL = "extramural"
