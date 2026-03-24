"""
Сервис для работы с приказами.
"""
from sqlalchemy.orm import Session
from uuid import UUID
from datetime import date
from typing import List, Dict, Any

from app.models.order import Order
from app.models.order_student import OrderStudent
from app.models.student import Student
from app.models.enrollment_order import EnrollmentOrder
from app.models.expulsion_order import ExpulsionOrder
from app.models.next_course_order import NextCourseOrder
from app.models.academic_leave_order import AcademicLeaveOrder
from app.models.transfer_direction_order import TransferDirectionOrder
from app.enums import OrderType, StudentStatus


class OrderService:
    """Сервис для управления приказами"""
    
    def __init__(self, db: Session):
        self.db = db
    
    def create_enrollment_order_with_students(
        self,
        order_data: Dict[str, Any],
        enrollment_data: Dict[str, Any],
        students_data: List[Dict[str, Any]]
    ):
        """Создать приказ о зачислении со студентами."""
        try:
            order = Order(
                number=order_data["number"],
                date=order_data["date"],
                type=OrderType.ENROLLMENT,
                reason=order_data["reason"]
            )
            self.db.add(order)
            self.db.flush()
            
            enrollment_order = EnrollmentOrder(
                order_id=order.id,
                education_form=enrollment_data.get("education_form", "full-time"),
                price=enrollment_data.get("price")
            )
            self.db.add(enrollment_order)
            
            created_students = []
            for student_data in students_data:
                student = Student(
                    name=student_data["name"],
                    last_name=student_data["last_name"],
                    patronymic=student_data.get("patronymic"),
                    study_book_number=student_data["study_book_number"],
                    group_id=student_data["group_id"],
                    enrollment_date=order_data["date"],
                    status=StudentStatus.STUDY
                )
                self.db.add(student)
                self.db.flush()
                
                order_student = OrderStudent(
                    order_id=order.id,
                    student_id=student.id
                )
                self.db.add(order_student)
                created_students.append(student)
            
            self.db.commit()
            self.db.refresh(order)
            
            return {"order": order, "enrollment_order": enrollment_order, "students": created_students}
            
        except Exception as e:
            self.db.rollback()
            raise e
    
    def create_expulsion_order(
        self,
        order_data: Dict[str, Any],
        expulsion_data: Dict[str, Any],
        student_ids: List[UUID]
    ):
        """Создать приказ об отчислении."""
        try:
            order = Order(
                number=order_data["number"],
                date=order_data["date"],
                type=OrderType.EXPULSION,
                reason=order_data["reason"]
            )
            self.db.add(order)
            self.db.flush()
            
            expulsion_order = ExpulsionOrder(
                order_id=order.id,
                expulsion_date=expulsion_data.get("expulsion_date"),
                expulsion_reason=expulsion_data.get("expulsion_reason")
            )
            self.db.add(expulsion_order)
            
            for student_id in student_ids:
                student = self.db.query(Student).filter(Student.id == student_id).first()
                if student:
                    student.status = StudentStatus.EXPELLED
                    student.expulsion_date = order_data["date"]
                    order_student = OrderStudent(order_id=order.id, student_id=student.id)
                    self.db.add(order_student)
            
            self.db.commit()
            self.db.refresh(order)
            return {"order": order, "expulsion_order": expulsion_order}
            
        except Exception as e:
            self.db.rollback()
            raise e
    
    def create_academic_leave_order(
        self,
        order_data: Dict[str, Any],
        academic_leave_data: Dict[str, Any],
        student_ids: List[UUID]
    ):
        """Создать приказ об академическом отпуске."""
        try:
            order = Order(
                number=order_data["number"],
                date=order_data["date"],
                type=OrderType.ACADEMIC_LEAVE,
                reason=order_data["reason"]
            )
            self.db.add(order)
            self.db.flush()
            
            academic_leave_order = AcademicLeaveOrder(
                order_id=order.id,
                leave_start=academic_leave_data.get("leave_start"),
                leave_end=academic_leave_data.get("leave_end"),
                leave_reason=academic_leave_data.get("leave_reason")
            )
            self.db.add(academic_leave_order)
            
            for student_id in student_ids:
                student = self.db.query(Student).filter(Student.id == student_id).first()
                if student:
                    student.status = StudentStatus.ACADEMIC_LEAVE
                    order_student = OrderStudent(order_id=order.id, student_id=student.id)
                    self.db.add(order_student)
            
            self.db.commit()
            self.db.refresh(order)
            return {"order": order, "academic_leave_order": academic_leave_order}
            
        except Exception as e:
            self.db.rollback()
            raise e
    
    def create_next_course_order(
        self,
        order_data: Dict[str, Any],
        next_course_data: Dict[str, Any],
        student_ids: List[UUID]
    ):
        """Создать приказ о переводе на следующий курс."""
        try:
            order = Order(
                number=order_data["number"],
                date=order_data["date"],
                type=OrderType.NEXT_COURSE,
                reason=order_data["reason"]
            )
            self.db.add(order)
            self.db.flush()

            next_course_order = NextCourseOrder(
                order_id=order.id,
                from_course=next_course_data.get("from_course"),
                to_course=next_course_data.get("to_course")
            )
            self.db.add(next_course_order)

            for student_id in student_ids:
                order_student = OrderStudent(order_id=order.id, student_id=student_id)
                self.db.add(order_student)

            self.db.commit()
            self.db.refresh(order)
            return {"order": order, "next_course_order": next_course_order}

        except Exception as e:
            self.db.rollback()
            raise e
    
    def create_transfer_direction_order(
        self,
        order_data: Dict[str, Any],
        transfer_data: Dict[str, Any],
        student_ids: List[UUID]
    ):
        """Создать приказ о переводе на другое направление."""
        try:
            order = Order(
                number=order_data["number"],
                date=order_data["date"],
                type=OrderType.TRANSFER_DIRECTION,
                reason=order_data["reason"]
            )
            self.db.add(order)
            self.db.flush()
            
            transfer_direction_order = TransferDirectionOrder(
                order_id=order.id,
                from_direction_id=transfer_data.get("from_direction_id"),
                to_direction_id=transfer_data.get("to_direction_id")
            )
            self.db.add(transfer_direction_order)
            
            for student_id in student_ids:
                order_student = OrderStudent(order_id=order.id, student_id=student.id)
                self.db.add(order_student)
            
            self.db.commit()
            self.db.refresh(order)
            return {"order": order, "transfer_direction_order": transfer_direction_order}
            
        except Exception as e:
            self.db.rollback()
            raise e
