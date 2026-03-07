from fastapi import FastAPI

from app.database.database import engine
from app.database.base import Base
from app.routers import auth

import app.models

app = FastAPI(title="Университет. Учёт контингента")

app.include_router(auth.router)


@app.get("/")
def root():
    return {"message": "API is working"}
