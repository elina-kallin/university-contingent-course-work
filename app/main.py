from fastapi import FastAPI

app = FastAPI(title="Университет. Учёт контингента")


@app.get("/")
def root():
    return {"message": "API is working"}
