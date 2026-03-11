from sqlalchemy import Column, ForeignKey, String
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class EnrollmentOrder(Base):
    __tablename__ = "enrollment_orders"

    order_id = Column(UUID(as_uuid=True), ForeignKey("orders.id"), primary_key=True)

    education_form = Column(String, nullable=False)

    price = Column(String)

    order = relationship("Order", back_populates="enrollment_order")
