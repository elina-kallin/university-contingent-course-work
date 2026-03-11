from fastapi import APIRouter, Depends
from typing import Dict, List
from app.enums import StudentStatus, OrderType, ExpulsionReason, AcademicLeaveReason, ControlType, EducationForm
from app.core.dependencies import get_current_dean
from app.models.dean import Dean

router = APIRouter(prefix="/enums", tags=["Enums"])


@router.get("/student-status")
def get_student_statuses(dean: Dean = Depends(get_current_dean)):
    return {"statuses": [{"value": s.value, "label": s.name.replace("_", " ").title()} for s in StudentStatus]}


@router.get("/order-types")
def get_order_types(dean: Dean = Depends(get_current_dean)):
    return {"types": [{"value": t.value, "label": t.name.replace("_", " ").title()} for t in OrderType]}


@router.get("/expulsion-reasons")
def get_expulsion_reasons(dean: Dean = Depends(get_current_dean)):
    return {"reasons": [{"value": r.value, "label": r.name.replace("_", " ").title()} for r in ExpulsionReason]}


@router.get("/academic-leave-reasons")
def get_academic_leave_reasons(dean: Dean = Depends(get_current_dean)):
    return {"reasons": [{"value": r.value, "label": r.name.replace("_", " ").title()} for r in AcademicLeaveReason]}


@router.get("/control-types")
def get_control_types(dean: Dean = Depends(get_current_dean)):
    return {"types": [{"value": t.value, "label": t.value} for t in ControlType]}


@router.get("/education-forms")
def get_education_forms(dean: Dean = Depends(get_current_dean)):
    return {"forms": [{"value": f.value, "label": f.value} for f in EducationForm]}


@router.get("/")
def get_all_enums(dean: Dean = Depends(get_current_dean)):
    return {
        "student_statuses": [{"value": s.value, "label": s.name.replace("_", " ").title()} for s in StudentStatus],
        "order_types": [{"value": t.value, "label": t.name.replace("_", " ").title()} for t in OrderType],
        "expulsion_reasons": [{"value": r.value, "label": r.name.replace("_", " ").title()} for r in ExpulsionReason],
        "academic_leave_reasons": [{"value": r.value, "label": r.name.replace("_", " ").title()} for r in AcademicLeaveReason],
        "control_types": [{"value": t.value, "label": t.value} for t in ControlType],
        "education_forms": [{"value": f.value, "label": f.value} for f in EducationForm]
    }
