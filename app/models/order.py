import uuid
from sqlalchemy import Column, Integer, String, Date
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class Order(Base):
    __tablename__ = "orders"

    id = Column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)

    number = Column(String, nullable=False)
    date = Column(Date, nullable=False)
    type = Column(String, nullable=False)
    reason = Column(String, nullable=False)

    students = relationship(
        "OrderStudent", back_populates="order", cascade="all, delete-orphan"
    )

    enrollment_order = relationship(
        "EnrollmentOrder", back_populates="order", uselist=False
    )

    expulsion_order = relationship(
        "ExpulsionOrder", back_populates="order", uselist=False
    )

    academic_leave_order = relationship(
        "AcademicLeaveOrder", back_populates="order", uselist=False
    )

    next_course_order = relationship(
        "NextCourseOrder", back_populates="order", uselist=False
    )

    transfer_direction_order = relationship(
        "TransferDirectionOrder", back_populates="order", uselist=False
    )
