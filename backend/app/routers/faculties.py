from fastapi import APIRouter, Depends, HTTPException
from sqlalchemy.orm import Session
from uuid import UUID
from typing import List

from app.database.database import get_db
from app.cruds import faculty as crud
from app.schemas.faculty import FacultyCreate, FacultyUpdate, FacultyRead
from app.core.dependencies import get_current_dean
from app.models.dean import Dean

router = APIRouter(prefix="/faculties", tags=["Faculties"])


@router.get("/", response_model=List[FacultyRead])
def get_faculties(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.get_faculties(db, skip, limit)


@router.get("/{faculty_id}", response_model=FacultyRead)
def get_faculty(faculty_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_faculty = crud.get_faculty(db, faculty_id)
    if not db_faculty:
        raise HTTPException(status_code=404, detail="Faculty not found")
    return db_faculty


@router.post("/", response_model=FacultyRead)
def create_faculty(faculty: FacultyCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.create_faculty(db, faculty)


@router.put("/{faculty_id}", response_model=FacultyRead)
def update_faculty(faculty_id: UUID, faculty: FacultyUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_faculty = crud.update_faculty(db, faculty_id, faculty)
    if not db_faculty:
        raise HTTPException(status_code=404, detail="Faculty not found")
    return db_faculty


@router.delete("/{faculty_id}")
def delete_faculty(faculty_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_faculty = crud.delete_faculty(db, faculty_id)
    if not db_faculty:
        raise HTTPException(status_code=404, detail="Faculty not found")
    return {"message": "Faculty deleted"}
