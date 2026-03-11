from fastapi import APIRouter, Depends, HTTPException
from sqlalchemy.orm import Session
from uuid import UUID
from typing import List

from app.database.database import get_db
from app.cruds import group as crud
from app.schemas.group import GroupCreate, GroupUpdate, GroupRead
from app.core.dependencies import get_current_dean
from app.models.dean import Dean

router = APIRouter(prefix="/groups", tags=["Groups"])


@router.get("/", response_model=List[GroupRead])
def get_groups(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.get_groups(db, skip, limit)


@router.get("/{group_id}", response_model=GroupRead)
def get_group(group_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_group = crud.get_group(db, group_id)
    if not db_group:
        raise HTTPException(status_code=404, detail="Group not found")
    return db_group


@router.post("/", response_model=GroupRead)
def create_group(group: GroupCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.create_group(db, group)


@router.put("/{group_id}", response_model=GroupRead)
def update_group(group_id: UUID, group: GroupUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_group = crud.update_group(db, group_id, group)
    if not db_group:
        raise HTTPException(status_code=404, detail="Group not found")
    return db_group


@router.delete("/{group_id}")
def delete_group(group_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_group = crud.delete_group(db, group_id)
    if not db_group:
        raise HTTPException(status_code=404, detail="Group not found")
    return {"message": "Group deleted"}


@router.get("/direction/{direction_id}", response_model=List[GroupRead])
def get_groups_by_direction(direction_id: UUID, skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.get_groups_by_direction(db, direction_id, skip, limit)
