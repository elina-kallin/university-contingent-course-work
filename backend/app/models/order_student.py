import uuid
from sqlalchemy import Column, ForeignKey
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class OrderStudent(Base):
    __tablename__ = "order_students"

    id = Column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)

    order_id = Column(UUID(as_uuid=True), ForeignKey("orders.id"), nullable=False)

    student_id = Column(UUID(as_uuid=True), ForeignKey("students.id"), nullable=False)

    order = relationship("Order", back_populates="students")

    student = relationship("Student", back_populates="orders")
