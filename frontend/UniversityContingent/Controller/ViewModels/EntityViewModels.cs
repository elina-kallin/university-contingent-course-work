using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public string Id { get; set; } = string.Empty;

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
            EducationForm = direction.EducationForm;
        }

        [Browsable(false)]
        public string Id { get; set; } = string.Empty;

        [DisplayName("Наименование")]
        public string? Name { get; set; } = string.Empty;

        [DisplayName("Код")]
        public string? Code { get; set; } = string.Empty;

        [Browsable(false)]
        public string FacultyId { get; set; } = string.Empty;

        [DisplayName("Форма обучения")]
        public string EducationFormName => EducationForm switch
        {
            EducationForm.full_time => "Очная",
            EducationForm.part_time => "Заочная",
            EducationForm.extramural => "Очно-заочная",
            _ => "Не указано"
        };

        [Browsable(false)]
        public EducationForm EducationForm { get; set; }
    }

    /// <summary>
    /// ViewModel для редактирования направления
    /// </summary>
    public class DirectionEditViewModel
    {
        [Browsable(false)]
        public string Id { get; set; } = string.Empty;

        [DisplayName("Наименование")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Код")]
        public string Code { get; set; } = string.Empty;

        [DisplayName("Факультет")]
        public string FacultyId { get; set; } = string.Empty;

        [DisplayName("Форма обучения")]
        public EducationForm EducationForm { get; set; } = EducationForm.full_time;
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
            Year = group.Year;
        }

        [Browsable(false)]
        public string Id { get; set; } = string.Empty;

        [DisplayName("Название группы")]
        public string? Name { get; set; } = string.Empty;

        [Browsable(false)]
        public string DirectionId { get; set; } = string.Empty;

        [DisplayName("Курс")]
        public int Course { get; set; }

        [DisplayName("Год набора")]
        public int Year { get; set; }
    }

    /// <summary>
    /// ViewModel для редактирования группы
    /// </summary>
    public class GroupEditViewModel
    {
        [Browsable(false)]
        public string Id { get; set; } = string.Empty;

        [DisplayName("Название группы")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Направление")]
        public string DirectionId { get; set; } = string.Empty;

        [DisplayName("Курс")]
        public int Course { get; set; } = 1;

        [DisplayName("Год набора")]
        public int Year { get; set; } = DateTime.Now.Year;
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
            FullName = student.FullName ?? "Не указано";
            BirthDate = student.BirthDate;
            GroupId = student.GroupId;
            EnrollmentDate = student.EnrollmentDate;
            Status = student.Status;
            EnrollmentOrderId = student.EnrollmentOrderId;
        }

        [Browsable(false)]
        public string Id { get; set; } = string.Empty;

        [DisplayName("ФИО")]
        public string? FullName { get; set; } = string.Empty;

        [DisplayName("Дата рождения")]
        public DateTime BirthDate { get; set; }

        [Browsable(false)]
        public string GroupId { get; set; } = string.Empty;

        [DisplayName("Дата зачисления")]
        public DateTime EnrollmentDate { get; set; }

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
        public string? EnrollmentOrderId { get; set; }
    }

    /// <summary>
    /// ViewModel для редактирования студента
    /// </summary>
    public class StudentEditViewModel
    {
        [Browsable(false)]
        public string Id { get; set; } = string.Empty;

        [DisplayName("ФИО")]
        public string FullName { get; set; } = string.Empty;

        [DisplayName("Дата рождения")]
        public DateTime BirthDate { get; set; } = DateTime.Now.AddYears(-18);

        [DisplayName("Группа")]
        public string GroupId { get; set; } = string.Empty;

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
        public int Id { get; set; }

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
}
