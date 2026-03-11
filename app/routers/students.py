from fastapi import APIRouter, Depends, HTTPException
from sqlalchemy.orm import Session
from uuid import UUID
from typing import List, Optional

from app.database.database import get_db
from app.cruds import student as crud
from app.schemas.student import StudentCreate, StudentUpdate, StudentRead
from app.core.dependencies import get_current_dean
from app.models.dean import Dean

router = APIRouter(prefix="/students", tags=["Students"])


@router.get("/", response_model=List[StudentRead])
def get_students(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.get_students(db, skip, limit)


@router.get("/{student_id}", response_model=StudentRead)
def get_student(student_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_student = crud.get_student(db, student_id)
    if not db_student:
        raise HTTPException(status_code=404, detail="Student not found")
    return db_student


@router.post("/", response_model=StudentRead)
def create_student(student: StudentCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.create_student(db, student)


@router.put("/{student_id}", response_model=StudentRead)
def update_student(student_id: UUID, student: StudentUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_student = crud.update_student(db, student_id, student)
    if not db_student:
        raise HTTPException(status_code=404, detail="Student not found")
    return db_student


@router.delete("/{student_id}")
def delete_student(student_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_student = crud.delete_student(db, student_id)
    if not db_student:
        raise HTTPException(status_code=404, detail="Student not found")
    return {"message": "Student deleted"}


@router.get("/group/{group_id}", response_model=List[StudentRead])
def get_students_by_group(group_id: UUID, skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return crud.get_students_by_group(db, group_id, skip, limit)


@router.get("/search", response_model=List[StudentRead])
def search_students(
    search_query: Optional[str] = None,
    group_id: Optional[UUID] = None,
    status: Optional[str] = None,
    skip: int = 0,
    limit: int = 100,
    db: Session = Depends(get_db),
    dean: Dean = Depends(get_current_dean)
):
    return crud.search_students(db, search_query, group_id, status, skip, limit)
