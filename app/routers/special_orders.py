from fastapi import APIRouter, Depends, HTTPException
from sqlalchemy.orm import Session
from uuid import UUID
from typing import List

from app.database.database import get_db
from app.cruds import enrollment_order as enrollment_crud
from app.cruds import expulsion_order as expulsion_crud
from app.cruds import next_course_order as next_course_crud
from app.cruds import academic_leave_order as academic_leave_crud
from app.cruds import transfer_direction_order as transfer_direction_crud
from app.services.order_service import OrderService
from app.schemas.enrollment_order import EnrollmentOrderCreate, EnrollmentOrderUpdate, EnrollmentOrderRead
from app.schemas.expulsion_order import ExpulsionOrderCreate, ExpulsionOrderUpdate, ExpulsionOrderRead
from app.schemas.next_course_order import NextCourseOrderCreate, NextCourseOrderUpdate, NextCourseOrderRead
from app.schemas.academic_leave_order import AcademicLeaveOrderCreate, AcademicLeaveOrderUpdate, AcademicLeaveOrderRead
from app.schemas.transfer_direction_order import TransferDirectionOrderCreate, TransferDirectionOrderUpdate, TransferDirectionOrderRead
from app.schemas.order_with_students import (
    EnrollmentOrderWithStudentsCreate,
    ExpulsionOrderWithStudentsCreate,
    AcademicLeaveOrderWithStudentsCreate,
    NextCourseOrderWithStudentsCreate,
    TransferDirectionOrderWithStudentsCreate
)
from app.core.dependencies import get_current_dean
from app.models.dean import Dean

router = APIRouter(prefix="/special-orders", tags=["Special Orders"])


# Enrollment Order endpoints
@router.get("/enrollment", response_model=List[EnrollmentOrderRead])
def get_enrollment_orders(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return enrollment_crud.get_enrollment_orders(db, skip, limit)


@router.get("/enrollment/{order_id}", response_model=EnrollmentOrderRead)
def get_enrollment_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = enrollment_crud.get_enrollment_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="EnrollmentOrder not found")
    return db_order


@router.post("/enrollment", response_model=EnrollmentOrderRead)
def create_enrollment_order(enrollment_order: EnrollmentOrderCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return enrollment_crud.create_enrollment_order(db, enrollment_order)


@router.put("/enrollment/{order_id}", response_model=EnrollmentOrderRead)
def update_enrollment_order(order_id: UUID, enrollment_order: EnrollmentOrderUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = enrollment_crud.update_enrollment_order(db, order_id, enrollment_order)
    if not db_order:
        raise HTTPException(status_code=404, detail="EnrollmentOrder not found")
    return db_order


@router.delete("/enrollment/{order_id}")
def delete_enrollment_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = enrollment_crud.delete_enrollment_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="EnrollmentOrder not found")
    return {"message": "EnrollmentOrder deleted"}


# Expulsion Order endpoints
@router.get("/expulsion", response_model=List[ExpulsionOrderRead])
def get_expulsion_orders(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return expulsion_crud.get_expulsion_orders(db, skip, limit)


@router.get("/expulsion/{order_id}", response_model=ExpulsionOrderRead)
def get_expulsion_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = expulsion_crud.get_expulsion_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="ExpulsionOrder not found")
    return db_order


@router.post("/expulsion", response_model=ExpulsionOrderRead)
def create_expulsion_order(expulsion_order: ExpulsionOrderCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return expulsion_crud.create_expulsion_order(db, expulsion_order)


@router.put("/expulsion/{order_id}", response_model=ExpulsionOrderRead)
def update_expulsion_order(order_id: UUID, expulsion_order: ExpulsionOrderUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = expulsion_crud.update_expulsion_order(db, order_id, expulsion_order)
    if not db_order:
        raise HTTPException(status_code=404, detail="ExpulsionOrder not found")
    return db_order


@router.delete("/expulsion/{order_id}")
def delete_expulsion_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = expulsion_crud.delete_expulsion_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="ExpulsionOrder not found")
    return {"message": "ExpulsionOrder deleted"}


# Next Course Order endpoints
@router.get("/next-course", response_model=List[NextCourseOrderRead])
def get_next_course_orders(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return next_course_crud.get_next_course_orders(db, skip, limit)


@router.get("/next-course/{order_id}", response_model=NextCourseOrderRead)
def get_next_course_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = next_course_crud.get_next_course_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="NextCourseOrder not found")
    return db_order


@router.post("/next-course", response_model=NextCourseOrderRead)
def create_next_course_order(next_course_order: NextCourseOrderCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return next_course_crud.create_next_course_order(db, next_course_order)


@router.put("/next-course/{order_id}", response_model=NextCourseOrderRead)
def update_next_course_order(order_id: UUID, next_course_order: NextCourseOrderUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = next_course_crud.update_next_course_order(db, order_id, next_course_order)
    if not db_order:
        raise HTTPException(status_code=404, detail="NextCourseOrder not found")
    return db_order


@router.delete("/next-course/{order_id}")
def delete_next_course_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = next_course_crud.delete_next_course_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="NextCourseOrder not found")
    return {"message": "NextCourseOrder deleted"}


# Academic Leave Order endpoints
@router.get("/academic-leave", response_model=List[AcademicLeaveOrderRead])
def get_academic_leave_orders(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return academic_leave_crud.get_academic_leave_orders(db, skip, limit)


@router.get("/academic-leave/{order_id}", response_model=AcademicLeaveOrderRead)
def get_academic_leave_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = academic_leave_crud.get_academic_leave_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="AcademicLeaveOrder not found")
    return db_order


@router.post("/academic-leave", response_model=AcademicLeaveOrderRead)
def create_academic_leave_order(academic_leave_order: AcademicLeaveOrderCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return academic_leave_crud.create_academic_leave_order(db, academic_leave_order)


@router.put("/academic-leave/{order_id}", response_model=AcademicLeaveOrderRead)
def update_academic_leave_order(order_id: UUID, academic_leave_order: AcademicLeaveOrderUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = academic_leave_crud.update_academic_leave_order(db, order_id, academic_leave_order)
    if not db_order:
        raise HTTPException(status_code=404, detail="AcademicLeaveOrder not found")
    return db_order


@router.delete("/academic-leave/{order_id}")
def delete_academic_leave_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = academic_leave_crud.delete_academic_leave_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="AcademicLeaveOrder not found")
    return {"message": "AcademicLeaveOrder deleted"}


# Transfer Direction Order endpoints
@router.get("/transfer-direction", response_model=List[TransferDirectionOrderRead])
def get_transfer_direction_orders(skip: int = 0, limit: int = 100, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return transfer_direction_crud.get_transfer_direction_orders(db, skip, limit)


@router.get("/transfer-direction/{order_id}", response_model=TransferDirectionOrderRead)
def get_transfer_direction_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = transfer_direction_crud.get_transfer_direction_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="TransferDirectionOrder not found")
    return db_order


@router.post("/transfer-direction", response_model=TransferDirectionOrderRead)
def create_transfer_direction_order(transfer_direction_order: TransferDirectionOrderCreate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    return transfer_direction_crud.create_transfer_direction_order(db, transfer_direction_order)


@router.put("/transfer-direction/{order_id}", response_model=TransferDirectionOrderRead)
def update_transfer_direction_order(order_id: UUID, transfer_direction_order: TransferDirectionOrderUpdate, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = transfer_direction_crud.update_transfer_direction_order(db, order_id, transfer_direction_order)
    if not db_order:
        raise HTTPException(status_code=404, detail="TransferDirectionOrder not found")
    return db_order


@router.delete("/transfer-direction/{order_id}")
def delete_transfer_direction_order(order_id: UUID, db: Session = Depends(get_db), dean: Dean = Depends(get_current_dean)):
    db_order = transfer_direction_crud.delete_transfer_direction_order(db, order_id)
    if not db_order:
        raise HTTPException(status_code=404, detail="TransferDirectionOrder not found")
    return {"message": "TransferDirectionOrder deleted"}


# ============================================
# НОВЫЕ ЭНДПОИНТЫ: Создание приказов со студентами
# ============================================

@router.post("/enrollment-with-students")
def create_enrollment_order_with_students(
    data: EnrollmentOrderWithStudentsCreate,
    db: Session = Depends(get_db),
    dean: Dean = Depends(get_current_dean)
):
    """
    Создать приказ о зачислении со студентами.
    Студенты создаются автоматически и привязываются к приказу.
    """
    service = OrderService(db)
    result = service.create_enrollment_order_with_students(
        order_data=data.order,
        enrollment_data=data.enrollment_order,
        students_data=[s.model_dump() for s in data.students]
    )
    return {
        "message": "Приказ о зачислении создан",
        "order_id": str(result["order"].id),
        "students_count": len(result["students"])
    }


@router.post("/expulsion-with-students")
def create_expulsion_order_with_students(
    data: ExpulsionOrderWithStudentsCreate,
    db: Session = Depends(get_db),
    dean: Dean = Depends(get_current_dean)
):
    """
    Создать приказ об отчислении.
    Статусы студентов обновляются на "expelled".
    """
    service = OrderService(db)
    result = service.create_expulsion_order(
        order_data=data.order,
        expulsion_data=data.expulsion_order,
        student_ids=data.student_ids
    )
    return {
        "message": "Приказ об отчислении создан",
        "order_id": str(result["order"].id)
    }


@router.post("/academic-leave-with-students")
def create_academic_leave_order_with_students(
    data: AcademicLeaveOrderWithStudentsCreate,
    db: Session = Depends(get_db),
    dean: Dean = Depends(get_current_dean)
):
    """
    Создать приказ об академическом отпуске.
    Статусы студентов обновляются на "academic_leave".
    """
    service = OrderService(db)
    result = service.create_academic_leave_order(
        order_data=data.order,
        academic_leave_data=data.academic_leave_order,
        student_ids=data.student_ids
    )
    return {
        "message": "Приказ об академическом отпуске создан",
        "order_id": str(result["order"].id)
    }


@router.post("/next-course-with-students")
def create_next_course_order_with_students(
    data: NextCourseOrderWithStudentsCreate,
    db: Session = Depends(get_db),
    dean: Dean = Depends(get_current_dean)
):
    """
    Создать приказ о переводе на следующий курс.
    """
    service = OrderService(db)
    result = service.create_next_course_order(
        order_data=data.order,
        next_course_data=data.next_course_order,
        student_ids=data.student_ids
    )
    return {
        "message": "Приказ о переводе на следующий курс создан",
        "order_id": str(result["order"].id)
    }


@router.post("/transfer-direction-with-students")
def create_transfer_direction_order_with_students(
    data: TransferDirectionOrderWithStudentsCreate,
    db: Session = Depends(get_db),
    dean: Dean = Depends(get_current_dean)
):
    """
    Создать приказ о переводе на другое направление.
    """
    service = OrderService(db)
    result = service.create_transfer_direction_order(
        order_data=data.order,
        transfer_data=data.transfer_direction_order,
        student_ids=data.student_ids
    )
    return {
        "message": "Приказ о переводе создан",
        "order_id": str(result["order"].id)
    }

