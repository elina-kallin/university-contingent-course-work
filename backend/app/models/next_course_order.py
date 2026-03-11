from sqlalchemy import Column, ForeignKey, Integer
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class NextCourseOrder(Base):
    __tablename__ = "next_course_orders"

    order_id = Column(UUID(as_uuid=True), ForeignKey("orders.id"), primary_key=True)

    from_course = Column(Integer)

    to_course = Column(Integer)

    order = relationship("Order", back_populates="next_course_order")
