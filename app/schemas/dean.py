from pydantic import BaseModel
from uuid import UUID


class DeanCreate(BaseModel):
    full_name: str
    login: str
    password: str
    faculty_id: UUID


class DeanRead(BaseModel):
    id: UUID
    full_name: str
    login: str
    faculty_id: UUID

    class Config:
        from_attributes = True


class DeanLogin(BaseModel):
    login: str
    password: str


class Token(BaseModel):
    access_token: str
    token_type: str
