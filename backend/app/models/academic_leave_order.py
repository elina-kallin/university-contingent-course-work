from sqlalchemy import Column, ForeignKey, Date, String, Enum
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base
from app.enums import AcademicLeaveReason


class AcademicLeaveOrder(Base):
    __tablename__ = "academic_leave_orders"

    order_id = Column(UUID(as_uuid=True), ForeignKey("orders.id"), primary_key=True)

    leave_start = Column(Date)

    leave_end = Column(Date)

    leave_reason = Column(Enum(AcademicLeaveReason))

    order = relationship("Order", back_populates="academic_leave_order")
