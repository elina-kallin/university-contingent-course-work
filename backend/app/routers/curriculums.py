from fastapi import APIRouter, Depends, HTTPException
from sqlalchemy.orm import Session
from uuid import UUID
from typing import List

from app.database.database import get_db
from app.cruds import curriculum as curriculum_crud
from app.cruds import discipline as discipline_crud
from app.schemas.curriculum import CurriculumCreate, CurriculumUpdate, CurriculumRead
from app.schemas.discipline import DisciplineCreate, DisciplineUpdate, DisciplineRead
from app.core.dependencies import get_current_dean
from app.models.dean import Dean

router = APIRouter(prefix="/curriculums", tags=["Curriculums", "Disciplines"])


# Curriculum endpoints
@router.get("/", response_model=List[CurriculumRead])
def get_curriculums(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return curriculum_crud.get_curriculums(db, skip, limit)


@router.get("/{curriculum_id}", response_model=CurriculumRead)
def get_curriculum(curriculum_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_curriculum = curriculum_crud.get_curriculum(db, curriculum_id)
    if not db_curriculum:
        raise HTTPException(status_code=404, detail="Curriculum not found")
    return db_curriculum


@router.get("/direction/{direction_id}", response_model=CurriculumRead)
def get_curriculum_by_direction(direction_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_curriculum = curriculum_crud.get_curriculum_by_direction(db, direction_id)
    if not db_curriculum:
        raise HTTPException(status_code=404, detail="Curriculum not found")
    return db_curriculum


@router.post("/", response_model=CurriculumRead)
def create_curriculum(curriculum: CurriculumCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return curriculum_crud.create_curriculum(db, curriculum)


@router.put("/{curriculum_id}", response_model=CurriculumRead)
def update_curriculum(curriculum_id: UUID, curriculum: CurriculumUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_curriculum = curriculum_crud.update_curriculum(db, curriculum_id, curriculum)
    if not db_curriculum:
        raise HTTPException(status_code=404, detail="Curriculum not found")
    return db_curriculum


@router.delete("/{curriculum_id}")
def delete_curriculum(curriculum_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_curriculum = curriculum_crud.delete_curriculum(db, curriculum_id)
    if not db_curriculum:
        raise HTTPException(status_code=404, detail="Curriculum not found")
    return {"message": "Curriculum deleted"}


# Discipline endpoints
@router.get("/disciplines", response_model=List[DisciplineRead])
def get_disciplines(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return discipline_crud.get_disciplines(db, skip, limit)


@router.get("/disciplines/{discipline_id}", response_model=DisciplineRead)
def get_discipline(discipline_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_discipline = discipline_crud.get_discipline(db, discipline_id)
    if not db_discipline:
        raise HTTPException(status_code=404, detail="Discipline not found")
    return db_discipline


@router.post("/disciplines", response_model=DisciplineRead)
def create_discipline(discipline: DisciplineCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return discipline_crud.create_discipline(db, discipline)


@router.put("/disciplines/{discipline_id}", response_model=DisciplineRead)
def update_discipline(discipline_id: UUID, discipline: DisciplineUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_discipline = discipline_crud.update_discipline(db, discipline_id, discipline)
    if not db_discipline:
        raise HTTPException(status_code=404, detail="Discipline not found")
    return db_discipline


@router.delete("/disciplines/{discipline_id}")
def delete_discipline(discipline_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_discipline = discipline_crud.delete_discipline(db, discipline_id)
    if not db_discipline:
        raise HTTPException(status_code=404, detail="Discipline not found")
    return {"message": "Discipline deleted"}


@router.get("/disciplines/curriculum/{curriculum_id}", response_model=List[DisciplineRead])
def get_disciplines_by_curriculum(curriculum_id: UUID, skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return discipline_crud.get_disciplines_by_curriculum(db, curriculum_id, skip, limit)


@router.get("/difference/{direction1_id}/{direction2_id}")
def get_curriculum_difference(
    direction1_id: UUID,
    direction2_id: UUID,
    db: Session = Depends(get_db),
    dean: Dean = Depends(get_current_dean)
):
    """
    Получить разницу в учебных планах между двумя направлениями.
    Используется при переводе студента на другое направление.
    """
    from app.models.curriculum import Curriculum
    from app.models.discipline import Discipline
    
    # Получаем учебные планы
    curriculum1 = db.query(Curriculum).filter(Curriculum.direction_id == direction1_id).first()
    curriculum2 = db.query(Curriculum).filter(Curriculum.direction_id == direction2_id).first()
    
    if not curriculum1:
        raise HTTPException(status_code=404, detail="Curriculum not found for direction 1")
    if not curriculum2:
        raise HTTPException(status_code=404, detail="Curriculum not found for direction 2")
    
    # Получаем дисциплины
    disciplines1 = db.query(Discipline).filter(Discipline.curriculum_id == curriculum1.id).all()
    disciplines2 = db.query(Discipline).filter(Discipline.curriculum_id == curriculum2.id).all()
    
    # Сравниваем по названию
    d1_names = {d.name: d for d in disciplines1}
    d2_names = {d.name: d for d in disciplines2}
    
    common = []
    only_in_first = []
    only_in_second = []
    
    for name, d in d1_names.items():
        if name in d2_names:
            common.append({
                "name": d.name,
                "semester": d.semester,
                "hours": d.hours,
                "control_type": d.control_type
            })
        else:
            only_in_first.append({
                "name": d.name,
                "semester": d.semester,
                "hours": d.hours,
                "control_type": d.control_type
            })
    
    for name, d in d2_names.items():
        if name not in d1_names:
            only_in_second.append({
                "name": d.name,
                "semester": d.semester,
                "hours": d.hours,
                "control_type": d.control_type
            })
    
    return {
        "direction1_id": str(direction1_id),
        "direction2_id": str(direction2_id),
        "common_disciplines": common,
        "only_in_first_direction": only_in_first,
        "only_in_second_direction": only_in_second,
        "summary": {
            "common_count": len(common),
            "to_study_count": len(only_in_second),
            "total_new": len(only_in_second)
        }
    }
