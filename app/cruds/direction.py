from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.direction import Direction
from app.schemas.direction import DirectionCreate, DirectionUpdate


def get_directions(db: Session, skip: int = 0, limit: int = 100) -> List[Direction]:
    return db.query(Direction).offset(skip).limit(limit).all()


def get_direction(db: Session, direction_id: UUID) -> Optional[Direction]:
    return db.query(Direction).filter(Direction.id == direction_id).first()


def create_direction(db: Session, direction: DirectionCreate) -> Direction:
    db_direction = Direction(**direction.model_dump())
    db.add(db_direction)
    db.commit()
    db.refresh(db_direction)
    return db_direction


def update_direction(db: Session, direction_id: UUID, direction: DirectionUpdate) -> Optional[Direction]:
    db_direction = get_direction(db, direction_id)
    if db_direction:
        update_data = direction.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_direction, field, value)
        db.commit()
        db.refresh(db_direction)
    return db_direction


def delete_direction(db: Session, direction_id: UUID) -> Optional[Direction]:
    db_direction = get_direction(db, direction_id)
    if db_direction:
        db.delete(db_direction)
        db.commit()
    return db_direction


def get_directions_by_faculty(db: Session, faculty_id: UUID, skip: int = 0, limit: int = 100) -> List[Direction]:
    return db.query(Direction).filter(Direction.faculty_id == faculty_id).offset(skip).limit(limit).all()
