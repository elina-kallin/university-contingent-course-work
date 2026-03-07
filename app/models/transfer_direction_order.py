from sqlalchemy import Column, ForeignKey
from sqlalchemy.dialects.postgresql import UUID
from sqlalchemy.orm import relationship

from app.database.base import Base


class TransferDirectionOrder(Base):
    __tablename__ = "transfer_direction_orders"

    order_id = Column(UUID(as_uuid=True), ForeignKey("orders.id"), primary_key=True)

    from_direction_id = Column(UUID(as_uuid=True), ForeignKey("directions.id"))

    to_direction_id = Column(UUID(as_uuid=True), ForeignKey("directions.id"))

    order = relationship("Order", back_populates="transfer_direction_order")

    from_direction = relationship("Direction", foreign_keys=[from_direction_id])

    to_direction = relationship("Direction", foreign_keys=[to_direction_id])
