using System.ComponentModel;
using UniversityContingent.Models;

namespace UniversityContingent.Controller.ViewModels
{
    /// <summary>
    /// ViewModel для факультета
    /// </summary>
    public class FacultyViewModel
    {
        public FacultyViewModel() { }

        public FacultyViewModel(Faculty faculty)
        {
            Id = faculty.Id;
            Name = faculty.Name ?? "Не указано";
            ShortName = faculty.ShortName ?? "Не указано";
        }

        [Browsable(false)]
        public Guid Id { get; set; }

        [DisplayName("Наименование")]
        public string? Name { get; set; } = string.Empty;

        [DisplayName("Краткое название")]
        public string? ShortName { get; set; } = string.Empty;
    }

    /// <summary>
    /// ViewModel для направления подготовки
    /// </summary>
    public class DirectionViewModel
    {
        public DirectionViewModel() { }

        public DirectionViewModel(Direction direction)
        {
            Id = direction.Id;
            Name = direction.Name ?? "Не указано";
            Code = direction.Code ?? "Не указано";
            FacultyId = direction.FacultyId;
            StudyDurationYears = direction.StudyDurationYears;
        }

        [Browsable(false)]
        public Guid Id { get; set; }

        [DisplayName("Наименование")]
        public string? Name { get; set; } = string.Empty;

        [DisplayName("Код")]
        public string? Code { get; set; } = string.Empty;

        [Browsable(false)]
        public Guid FacultyId { get; set; }

        [DisplayName("Срок обучения (лет)")]
        public int StudyDurationYears { get; set; }
    }

    /// <summary>
    /// ViewModel для редактирования направления
    /// </summary>
    public class DirectionEditViewModel
    {
        [Browsable(false)]
        public Guid Id { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Код")]
        public string Code { get; set; } = string.Empty;

        [DisplayName("Факультет")]
        public Guid FacultyId { get; set; }

        [DisplayName("Срок обучения (лет)")]
        public int StudyDurationYears { get; set; } = 4;
    }

    /// <summary>
    /// ViewModel для учебной группы
    /// </summary>
    public class GroupViewModel
    {
        public GroupViewModel() { }

        public GroupViewModel(Group group)
        {
            Id = group.Id;
            Name = group.Name ?? "Не указано";
            DirectionId = group.DirectionId;
            Course = group.Course;
        }

        [Browsable(false)]
        public Guid Id { get; set; }

        [DisplayName("Название группы")]
        public string? Name { get; set; } = string.Empty;

        [Browsable(false)]
        public Guid DirectionId { get; set; }

        [DisplayName("Курс")]
        public int Course { get; set; }
    }

    /// <summary>
    /// ViewModel для редактирования группы
    /// </summary>
    public class GroupEditViewModel
    {
        [Browsable(false)]
        public Guid Id { get; set; }

        [DisplayName("Название группы")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Направление")]
        public Guid DirectionId { get; set; }

        [DisplayName("Курс")]
        public int Course { get; set; } = 1;
    }

    /// <summary>
    /// ViewModel для студента
    /// </summary>
    public class StudentViewModel
    {
        public StudentViewModel() { }

        public StudentViewModel(Student student)
        {
            Id = student.Id;
            LastName = student.LastName ?? string.Empty;
            Name = student.Name ?? string.Empty;
            Patronymic = student.Patronymic ?? string.Empty;
            StudyBookNumber = student.StudyBookNumber;
            EnrollmentDate = student.EnrollmentDate ?? DateTime.Now;
            Status = student.Status;
            GroupId = student.GroupId;
        }

        [Browsable(false)]
        public Guid Id { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("Имя")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Отчество")]
        public string Patronymic { get; set; } = string.Empty;

        [DisplayName("Номер зачетки")]
        public int StudyBookNumber { get; set; }

        [DisplayName("Дата зачисления")]
        public DateTime EnrollmentDate { get; set; }

        [Browsable(false)]
        public Guid GroupId { get; set; }

        [DisplayName("Статус")]
        public string StatusName => Status switch
        {
            StudentStatus.study => "Обучается",
            StudentStatus.expelled => "Отчислен",
            StudentStatus.academic_leave => "Академический отпуск",
            _ => "Не указано"
        };

        [Browsable(false)]
        public StudentStatus Status { get; set; }

        [Browsable(false)]
        public string FullName => $"{LastName} {Name} {Patronymic}".Trim();
    }

    /// <summary>
    /// ViewModel для редактирования студента
    /// </summary>
    public class StudentEditViewModel
    {
        [Browsable(false)]
        public Guid Id { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("Имя")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Отчество")]
        public string Patronymic { get; set; } = string.Empty;

        [DisplayName("Группа")]
        public Guid GroupId { get; set; }

        [DisplayName("Дата зачисления")]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// ViewModel для приказа
    /// </summary>
    public class OrderViewModel
    {
        public OrderViewModel() { }

        public OrderViewModel(Order order)
        {
            Id = order.Id;
            Type = order.Type;
            Number = order.Number ?? "Не указан";
            Date = order.Date;
            Reason = order.Reason ?? "Не указана";
            CreatedAt = order.CreatedAt;
        }

        [Browsable(false)]
        public Guid Id { get; set; }

        [DisplayName("Тип")]
        public string TypeName => Type switch
        {
            OrderType.enrollment => "О зачислении",
            OrderType.expulsion => "Об отчислении",
            OrderType.next_course => "О переводе на следующий курс",
            OrderType.academic_leave => "О предоставлении академического отпуска",
            OrderType.transfer_direction => "О переводе на другое направление",
            _ => "Не указано"
        };

        [Browsable(false)]
        public OrderType Type { get; set; }

        [DisplayName("Номер")]
        public string? Number { get; set; } = string.Empty;

        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Причина")]
        public string? Reason { get; set; } = string.Empty;

        [DisplayName("Дата создания")]
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// ViewModel для создания приказа о зачислении
    /// </summary>
    public class EnrollmentOrderCreateViewModel
    {
        public EnrollmentOrderCreateViewModel()
        {
            Order = new Order();
            Students = new List<EnrollmentStudentViewModel>();
        }

        [Browsable(false)]
        public Order Order { get; set; }

        [Browsable(false)]
        public List<EnrollmentStudentViewModel> Students { get; set; }

        [DisplayName("Номер приказа")]
        public string Number => Order.Number ?? string.Empty;

        [DisplayName("Дата приказа")]
        public DateTime Date => Order.Date;

        [DisplayName("Количество студентов")]
        public int StudentCount => Students.Count;
    }

    /// <summary>
    /// ViewModel для студента в приказе о зачислении
    /// </summary>
    public class EnrollmentStudentViewModel
    {
        [DisplayName("Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("Имя")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Отчество")]
        public string Patronymic { get; set; } = string.Empty;

        [DisplayName("Номер зачетки")]
        public int StudyBookNumber { get; set; }

        [DisplayName("Группа")]
        public Guid GroupId { get; set; }

        [Browsable(false)]
        public string FullName => $"{LastName} {Name} {Patronymic}".Trim();
    }
}
