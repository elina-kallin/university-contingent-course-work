import uuid
from sqlalchemy import Column, String, ForeignKey
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class Dean(Base):
    __tablename__ = "deans"

    id = Column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)

    full_name = Column(String, nullable=False)

    login = Column(String, nullable=False, unique=True)
    password_hash = Column(String, nullable=False)

    faculty_id = Column(UUID(as_uuid=True), ForeignKey("faculties.id"))

    faculty = relationship("Faculty", back_populates="dean", uselist=False)
