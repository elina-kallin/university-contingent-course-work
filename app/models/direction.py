import uuid
from sqlalchemy import Column, String, Integer, ForeignKey
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class Direction(Base):
    __tablename__ = "directions"

    id = Column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)

    name = Column(String, nullable=False)
    code = Column(String, nullable=False)

    study_duration_years = Column(Integer, nullable=False)

    faculty_id = Column(UUID(as_uuid=True), ForeignKey("faculties.id"))

    faculty = relationship("Faculty", back_populates="directions")

    groups = relationship(
        "Group", back_populates="direction", cascade="all, delete-orphan"
    )

    curriculum = relationship("Curriculum", back_populates="direction", uselist=False)
