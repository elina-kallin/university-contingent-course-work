from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.faculty import Faculty
from app.schemas.faculty import FacultyCreate, FacultyUpdate


def get_faculties(db: Session, skip: int = 0, limit: int = 100) -> List[Faculty]:
    return db.query(Faculty).offset(skip).limit(limit).all()


def get_faculty(db: Session, faculty_id: UUID) -> Optional[Faculty]:
    return db.query(Faculty).filter(Faculty.id == faculty_id).first()


def create_faculty(db: Session, faculty: FacultyCreate) -> Faculty:
    db_faculty = Faculty(**faculty.model_dump())
    db.add(db_faculty)
    db.commit()
    db.refresh(db_faculty)
    return db_faculty


def update_faculty(db: Session, faculty_id: UUID, faculty: FacultyUpdate) -> Optional[Faculty]:
    db_faculty = get_faculty(db, faculty_id)
    if db_faculty:
        update_data = faculty.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_faculty, field, value)
        db.commit()
        db.refresh(db_faculty)
    return db_faculty


def delete_faculty(db: Session, faculty_id: UUID) -> Optional[Faculty]:
    db_faculty = get_faculty(db, faculty_id)
    if db_faculty:
        db.delete(db_faculty)
        db.commit()
    return db_faculty
