import uuid
from sqlalchemy import Column, String, Integer, ForeignKey
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class Group(Base):
    __tablename__ = "groups"

    id = Column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)

    name = Column(String, nullable=False)
    course = Column(Integer, nullable=False)

    direction_id = Column(UUID(as_uuid=True), ForeignKey("directions.id"))

    direction = relationship("Direction", back_populates="groups")

    students = relationship(
        "Student", back_populates="group", cascade="all, delete-orphan"
    )
