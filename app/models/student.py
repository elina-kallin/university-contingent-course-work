import uuid
from sqlalchemy import Column, String, Integer, ForeignKey, Date
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class Student(Base):
    __tablename__ = "students"

    id = Column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)

    name = Column(String, nullable=False)
    last_name = Column(String, nullable=False)
    patronymic = Column(String)
    study_book_number = Column(Integer, nullable=False)
    enrollment_date = Column(Date)
    expulsion_date = Column(Date)
    status = Column(String, nullable=False)

    group_id = Column(UUID(as_uuid=True), ForeignKey("groups.id"))
    group = relationship("Group", back_populates="students")
    orders = relationship("OrderStudent", back_populates="student")
