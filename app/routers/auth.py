from fastapi import APIRouter, Depends, HTTPException
from sqlalchemy.orm import Session

from app.database.database import get_db
from app.schemas.dean import DeanLogin, Token
from app.cruds.dean import get_dean_by_login
from app.core.security import verify_password
from app.core.jwt import create_access_token

router = APIRouter(prefix="/auth", tags=["Auth"])


@router.post("/login", response_model=Token)
def login(data: DeanLogin, db: Session = Depends(get_db)):
    dean = get_dean_by_login(db, data.login)

    if not dean:
        raise HTTPException(status_code=401, detail="Invalid login")

    if not verify_password(data.password, dean.password_hash):
        raise HTTPException(status_code=401, detail="Invalid password")

    token = create_access_token({"sub": str(dean.id)})

    return {"access_token": token, "token_type": "bearer"}
