import uuid
from sqlalchemy import Column, ForeignKey, Integer, String, Enum
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base
from app.enums import ControlType


class Discipline(Base):
    __tablename__ = "disciplines"

    id = Column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)

    name = Column(String, nullable=False)
    semester = Column(Integer)
    hours = Column(Integer)
    control_type = Column(Enum(ControlType), nullable=False)

    curriculum_id = Column(
        UUID(as_uuid=True), ForeignKey("curriculums.id"), nullable=False
    )

    curriculum = relationship("Curriculum", back_populates="disciplines", uselist=False)
