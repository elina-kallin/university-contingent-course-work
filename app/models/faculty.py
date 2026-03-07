import uuid
from sqlalchemy import Column, String
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class Faculty(Base):
    __tablename__ = "faculties"

    id = Column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)

    name = Column(String, nullable=False)
    short_name = Column(String, nullable=False)

    directions = relationship(
        "Direction", back_populates="faculty", cascade="all, delete-orphan"
    )
    dean = relationship("Dean", back_populates="faculty", uselist=False)
