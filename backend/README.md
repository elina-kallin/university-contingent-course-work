# University Contingent - Backend API

Бэкенд для системы учёта контингента университета.

## 📚 Стек

- **Backend**: Python, FastAPI, SQLAlchemy, Alembic
- **Database**: PostgreSQL (PostgresPro)
- **Auth**: JWT tokens

## 🚀 Быстрый старт

### 1. Установка зависимостей

```bash
cd backend
d:\code\university-contingent-course-work\.venv\Scripts\activate
pip install -r requirements.txt
```

### 2. Запуск сервера

```bash
uvicorn app.main:app --reload --host 0.0.0.0 --port 8000
```

### 3. Swagger UI

Откройте http://localhost:8000/docs

### 4. Авторизация

1. `POST /auth/login`
   ```json
   {"login": "dean", "password": "dean123"}
   ```
2. Используйте токен: `Authorization: Bearer <token>`

## 📋 Основные эндпоинты

| Метод | Эндпоинт | Описание |
|-------|----------|----------|
| POST | `/auth/login` | Получить JWT токен |
| GET | `/enums/` | Все перечисления |
| GET/POST | `/faculties` | Факультеты |
| GET/POST | `/directions` | Направления |
| GET/POST | `/groups` | Группы |
| GET/POST | `/students` | Студенты |
| POST | `/special-orders/enrollment-with-students` | Зачисление |
| GET | `/orders/{id}/print` | Печать приказа |
| GET | `/curriculums/difference/{dir1}/{dir2}` | Разница планов |
| GET | `/reports/contingent` | Отчёт по контингенту |

## 🔧 Миграции Alembic

```bash
alembic revision --autogenerate -m "Description"
alembic upgrade head
```

## 📁 Структура

```
backend/
├── app/
│   ├── core/           # Конфигурация, JWT, security
│   ├── cruds/          # CRUD операции
│   ├── database/       # Подключение к БД
│   ├── models/         # SQLAlchemy модели
│   ├── routers/        # API endpoints
│   ├── schemas/        # Pydantic схемы
│   ├── services/       # Бизнес-логика
│   ├── enums.py        # Перечисления
│   └── main.py         # Точка входа
├── alembic/            # Миграции
├── alembic.ini
├── requirements.txt
└── API_DOCS.md         # Полная документация
```

## 📞 Контакты

Елена - Курсовая работа по дисциплине ПиАПС
