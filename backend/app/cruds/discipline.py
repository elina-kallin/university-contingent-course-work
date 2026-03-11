from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.discipline import Discipline
from app.schemas.discipline import DisciplineCreate, DisciplineUpdate


def get_disciplines(db: Session, skip: int = 0, limit: int = 100) -> List[Discipline]:
    return db.query(Discipline).offset(skip).limit(limit).all()


def get_discipline(db: Session, discipline_id: UUID) -> Optional[Discipline]:
    return db.query(Discipline).filter(Discipline.id == discipline_id).first()


def create_discipline(db: Session, discipline: DisciplineCreate) -> Discipline:
    db_discipline = Discipline(**discipline.model_dump())
    db.add(db_discipline)
    db.commit()
    db.refresh(db_discipline)
    return db_discipline


def update_discipline(db: Session, discipline_id: UUID, discipline: DisciplineUpdate) -> Optional[Discipline]:
    db_discipline = get_discipline(db, discipline_id)
    if db_discipline:
        update_data = discipline.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_discipline, field, value)
        db.commit()
        db.refresh(db_discipline)
    return db_discipline


def delete_discipline(db: Session, discipline_id: UUID) -> Optional[Discipline]:
    db_discipline = get_discipline(db, discipline_id)
    if db_discipline:
        db.delete(db_discipline)
        db.commit()
    return db_discipline


def get_disciplines_by_curriculum(db: Session, curriculum_id: UUID, skip: int = 0, limit: int = 100) -> List[Discipline]:
    return db.query(Discipline).filter(Discipline.curriculum_id == curriculum_id).offset(skip).limit(limit).all()
