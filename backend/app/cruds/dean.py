from uuid import UUID

from sqlalchemy.orm import Session
from app.models.dean import Dean
from app.schemas.dean import DeanCreate
from app.core.security import hash_password


def create_dean(db: Session, dean: DeanCreate):
    db_dean = Dean(
        full_name=dean.full_name,
        login=dean.login,
        password_hash=hash_password(dean.password),
        faculty_id=dean.faculty_id,
    )

    db.add(db_dean)
    db.commit()
    db.refresh(db_dean)

    return db_dean


def get_dean_by_login(db: Session, login: str):
    return db.query(Dean).filter(Dean.login == login).first()


def get_dean_by_id(db: Session, id: UUID):
    return db.query(Dean).filter(Dean.id == id).first()
