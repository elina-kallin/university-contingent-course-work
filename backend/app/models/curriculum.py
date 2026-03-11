import uuid
from sqlalchemy import Column, ForeignKey
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class Curriculum(Base):
    __tablename__ = "curriculums"

    id = Column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)

    direction_id = Column(
        UUID(as_uuid=True), ForeignKey("directions.id"), nullable=False
    )

    direction = relationship("Direction", back_populates="curriculum", uselist=False)

    disciplines = relationship("Discipline", back_populates="curriculum")
