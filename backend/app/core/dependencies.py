from fastapi import Depends, HTTPException, status
from fastapi.security import OAuth2PasswordBearer, HTTPBearer, HTTPAuthorizationCredentials
from jose import jwt, JWTError
from sqlalchemy.orm import Session
from uuid import UUID

from app.database.database import get_db
from app.models.dean import Dean
from app.core.jwt import SECRET_KEY, ALGORITHM

# OAuth2 для Swagger UI (username/password форма)
oauth2_scheme = OAuth2PasswordBearer(tokenUrl="/auth/login", auto_error=False)
# Bearer токен для API
http_bearer = HTTPBearer(auto_error=False)


def get_current_dean(
    db: Session = Depends(get_db),
    oauth2_token: str = Depends(oauth2_scheme),
    bearer: HTTPAuthorizationCredentials = Depends(http_bearer)
):
    # Пробуем получить токен из Bearer заголовка
    token = None
    if bearer:
        token = bearer.credentials
    elif oauth2_token:
        token = oauth2_token
    
    if not token:
        raise HTTPException(
            status_code=status.HTTP_401_UNAUTHORIZED,
            detail="Not authenticated",
            headers={"WWW-Authenticate": "Bearer"},
        )
    
    credentials_exception = HTTPException(
        status_code=status.HTTP_401_UNAUTHORIZED,
        detail="Could not validate credentials",
        headers={"WWW-Authenticate": "Bearer"},
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
