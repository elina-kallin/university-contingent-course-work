import uuid
from sqlalchemy import Column, String, ForeignKey
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class Dean(Base):
    __tablename__ = "deans"

    id = Column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)

    full_name = Column(String, nullable=False)
    user_id = Column(UUID(as_uuid=True), ForeignKey("users.id"))
    faculty_id = Column(UUID(as_uuid=True), ForeignKey("faculties.id"))

    user = relationship("User", back_populates="dean", uselist=False)
    faculty = relationship("Faculty", back_populates="dean", uselist=False)
