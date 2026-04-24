from fastapi import APIRouter, Depends
from sqlalchemy.orm import Session
from app.models.dean import Dean
from app.core.dependencies import get_current_dean, get_db

router = APIRouter(prefix="/deans", tags=["Deans"])

@router.get("/current_dean")
def get_current_dean(current_dean: Dean = Depends(get_current_dean))
