from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.curriculum import Curriculum
from app.schemas.curriculum import CurriculumCreate, CurriculumUpdate


def get_curriculums(db: Session, skip: int = 0, limit: int = 100) -> List[Curriculum]:
    return db.query(Curriculum).offset(skip).limit(limit).all()


def get_curriculum(db: Session, curriculum_id: UUID) -> Optional[Curriculum]:
    return db.query(Curriculum).filter(Curriculum.id == curriculum_id).first()


def get_curriculum_by_direction(db: Session, direction_id: UUID) -> Optional[Curriculum]:
    return db.query(Curriculum).filter(Curriculum.direction_id == direction_id).first()


def create_curriculum(db: Session, curriculum: CurriculumCreate) -> Curriculum:
    db_curriculum = Curriculum(**curriculum.model_dump())
    db.add(db_curriculum)
    db.commit()
    db.refresh(db_curriculum)
    return db_curriculum


def update_curriculum(db: Session, curriculum_id: UUID, curriculum: CurriculumUpdate) -> Optional[Curriculum]:
    db_curriculum = get_curriculum(db, curriculum_id)
    if db_curriculum:
        update_data = curriculum.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_curriculum, field, value)
        db.commit()
        db.refresh(db_curriculum)
    return db_curriculum


def delete_curriculum(db: Session, curriculum_id: UUID) -> Optional[Curriculum]:
    db_curriculum = get_curriculum(db, curriculum_id)
    if db_curriculum:
        db.delete(db_curriculum)
        db.commit()
    return db_curriculum
