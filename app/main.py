from fastapi import FastAPI

from app.database.database import engine
from app.database.base import Base

import app.models

app = FastAPI(title="Университет. Учёт контингента")


@app.get("/")
def root():
    return {"message": "API is working"}
