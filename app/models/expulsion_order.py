from sqlalchemy import Column, ForeignKey, String, Date
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class ExpulsionOrder(Base):
    __tablename__ = "expulsion_orders"

    order_id = Column(UUID(as_uuid=True), ForeignKey("orders.id"), primary_key=True)

    expulsion_date = Column(Date)

    expulsion_reason = Column(String)

    order = relationship("Order", back_populates="expulsion_order")
