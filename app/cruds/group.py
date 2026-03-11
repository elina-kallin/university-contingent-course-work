from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.models.group import Group
from app.schemas.group import GroupCreate, GroupUpdate


def get_groups(db: Session, skip: int = 0, limit: int = 100) -> List[Group]:
    return db.query(Group).offset(skip).limit(limit).all()


def get_group(db: Session, group_id: UUID) -> Optional[Group]:
    return db.query(Group).filter(Group.id == group_id).first()


def create_group(db: Session, group: GroupCreate) -> Group:
    db_group = Group(**group.model_dump())
    db.add(db_group)
    db.commit()
    db.refresh(db_group)
    return db_group


def update_group(db: Session, group_id: UUID, group: GroupUpdate) -> Optional[Group]:
    db_group = get_group(db, group_id)
    if db_group:
        update_data = group.model_dump(exclude_unset=True)
        for field, value in update_data.items():
            setattr(db_group, field, value)
        db.commit()
        db.refresh(db_group)
    return db_group


def delete_group(db: Session, group_id: UUID) -> Optional[Group]:
    db_group = get_group(db, group_id)
    if db_group:
        db.delete(db_group)
        db.commit()
    return db_group


def get_groups_by_direction(db: Session, direction_id: UUID, skip: int = 0, limit: int = 100) -> List[Group]:
    return db.query(Group).filter(Group.direction_id == direction_id).offset(skip).limit(limit).all()
