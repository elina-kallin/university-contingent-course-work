from fastapi import APIRouter, Depends, HTTPException
from sqlalchemy.orm import Session
from uuid import UUID
from typing import List

from app.database.database import get_db
from app.cruds import direction as crud
from app.schemas.direction import DirectionCreate, DirectionUpdate, DirectionRead
from app.core.dependencies import get_current_dean
from app.models.dean import Dean

router = APIRouter(prefix="/directions", tags=["Directions"])


@router.get("/", response_model=List[DirectionRead])
def get_directions(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.get_directions(db, skip, limit)


@router.get("/{direction_id}", response_model=DirectionRead)
def get_direction(direction_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_direction = crud.get_direction(db, direction_id)
    if not db_direction:
        raise HTTPException(status_code=404, detail="Direction not found")
    return db_direction


@router.post("/", response_model=DirectionRead)
def create_direction(direction: DirectionCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.create_direction(db, direction)


@router.put("/{direction_id}", response_model=DirectionRead)
def update_direction(direction_id: UUID, direction: DirectionUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_direction = crud.update_direction(db, direction_id, direction)
    if not db_direction:
        raise HTTPException(status_code=404, detail="Direction not found")
    return db_direction


@router.delete("/{direction_id}")
def delete_direction(direction_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_direction = crud.delete_direction(db, direction_id)
    if not db_direction:
        raise HTTPException(status_code=404, detail="Direction not found")
    return {"message": "Direction deleted"}


@router.get("/faculty/{faculty_id}", response_model=List[DirectionRead])
def get_directions_by_faculty(faculty_id: UUID, skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.get_directions_by_faculty(db, faculty_id, skip, limit)
