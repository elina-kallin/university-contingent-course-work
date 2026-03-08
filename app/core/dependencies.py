from fastapi import Depends, HTTPException, status
from fastapi.security import OAuth2PasswordBearer
from jose import jwt, JWTError
from sqlalchemy.orm import Session
from uuid import UUID

from app.database.database import get_db
from app.models.dean import Dean
from app.core.jwt import SECRET_KEY, ALGORITHM

oauth2_scheme = OAuth2PasswordBearer(tokenUrl="/auth/login")


def get_current_dean(
    token: str = Depends(oauth2_scheme), db: Session = Depends(get_db)
):
    credentials_exception = HTTPException(
        status_code=status.HTTP_401_UNAUTHORIZED,
        detail="Could not validate credentials",
    )

    try:
        payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])

        dean_id: str = payload.get("sub")

        if dean_id is None:
            raise credentials_exception

        dean_id = UUID(dean_id)

    except JWTError:
        raise credentials_exception

    dean = db.query(Dean).filter(Dean.id == dean_id).first()

    if dean is None:
        raise credentials_exception

    return dean
