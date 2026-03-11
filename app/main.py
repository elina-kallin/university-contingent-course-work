from fastapi import FastAPI
from fastapi.security import HTTPBearer

from app.database.database import engine
from app.database.base import Base
from app.routers import auth
from app.routers import faculties
from app.routers import directions
from app.routers import groups
from app.routers import students
from app.routers import curriculums
from app.routers import orders
from app.routers import special_orders
from app.routers import reports
from app.routers import enums

import app.models

security = HTTPBearer(description="JWT токен для авторизации")

app = FastAPI(
    title="Университет. Учёт контингента",
    description="API для системы учёта контингента университета",
    version="1.0.0",
    openapi_tags=[
        {"name": "Auth", "description": "Авторизация - получите токен через POST /auth/login"},
        {"name": "Enums", "description": "Перечисления (справочники)"},
        {"name": "Faculties", "description": "Факультеты"},
        {"name": "Directions", "description": "Направления"},
        {"name": "Groups", "description": "Группы"},
        {"name": "Students", "description": "Студенты"},
        {"name": "Curriculums", "description": "Учебные планы"},
        {"name": "Orders", "description": "Приказы"},
        {"name": "Special Orders", "description": "Специальные приказы (зачисление, отчисление, академ, перевод)"},
        {"name": "Reports", "description": "Отчёты"},
    ],
    security_schemes={
        "BearerAuth": {
            "type": "http",
            "scheme": "bearer",
            "bearerFormat": "JWT",
            "description": "Вставьте JWT токен, полученный через /auth/login"
        }
    }
)

app.include_router(auth.router)
app.include_router(enums.router)
app.include_router(faculties.router)
app.include_router(directions.router)
app.include_router(groups.router)
app.include_router(students.router)
app.include_router(curriculums.router)
app.include_router(orders.router)
app.include_router(special_orders.router)
app.include_router(reports.router)


@app.get("/")
def root():
    return {"message": "API is working. Откройте /docs для документации."}
