using System.Text.Json.Serialization;

namespace UniversityContingent.Models
{
    /// <summary>
    /// Факультет
    /// </summary>
    public class Faculty
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("short_name")]
        public string? ShortName { get; set; }
    }

    /// <summary>
    /// Направление подготовки
    /// </summary>
    public class Direction
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("study_duration_years")]
        public int StudyDurationYears { get; set; }

        [JsonPropertyName("faculty_id")]
        public Guid FacultyId { get; set; }
    }

    /// <summary>
    /// Учебная группа
    /// </summary>
    public class Group
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("course")]
        public int Course { get; set; }

        [JsonPropertyName("direction_id")]
        public Guid DirectionId { get; set; }
    }

    /// <summary>
    /// Студент
    /// </summary>
    public class Student
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("patronymic")]
        public string? Patronymic { get; set; }

        [JsonPropertyName("study_book_number")]
        public int StudyBookNumber { get; set; }

        [JsonPropertyName("enrollment_date")]
        public DateTime? EnrollmentDate { get; set; }

        [JsonPropertyName("expulsion_date")]
        public DateTime? ExpulsionDate { get; set; }

        [JsonPropertyName("status")]
        public StudentStatus Status { get; set; }

        [JsonPropertyName("group_id")]
        public Guid GroupId { get; set; }

        // Вспомогательное свойство для отображения
        [JsonIgnore]
        public string FullName => $"{LastName} {Name} {Patronymic}".Trim();
    }

    /// <summary>
    /// Приказ
    /// </summary>
    public class Order
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("number")]
        public string? Number { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("type")]
        public OrderType Type { get; set; }

        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        [JsonPropertyName("student_ids")]
        public List<Guid> StudentIds { get; set; } = new();

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Данные для зачисления студента
    /// </summary>
    public class EnrollmentStudent
    {
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("patronymic")]
        public string? Patronymic { get; set; }

        [JsonPropertyName("study_book_number")]
        public int StudyBookNumber { get; set; }

        [JsonPropertyName("group_id")]
        public Guid GroupId { get; set; }
    }

    /// <summary>
    /// Студент для зачисления (соответствует StudentForEnrollment на бэкенде)
    /// </summary>
    public class StudentForEnrollment
    {
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("patronymic")]
        public string? Patronymic { get; set; }

        [JsonPropertyName("study_book_number")]
        public int StudyBookNumber { get; set; }

        [JsonPropertyName("group_id")]
        public Guid GroupId { get; set; }
    }

    /// <summary>
    /// Данные для приказа о зачислении (enrollment_order)
    /// </summary>
    public class EnrollmentOrderCreateData
    {
        [JsonPropertyName("order_id")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("education_form")]
        public string EducationForm { get; set; } = "full-time";

        [JsonPropertyName("price")]
        public string? Price { get; set; }
    }

    /// <summary>
    /// Данные для приказа о зачислении со студентами
    /// </summary>
    public class EnrollmentOrderWithStudentsCreate
    {
        [JsonPropertyName("order")]
        public OrderCreateData Order { get; set; } = new();

        [JsonPropertyName("enrollment_order")]
        public EnrollmentOrderCreateData EnrollmentOrder { get; set; } = new();

        [JsonPropertyName("students")]
        public List<StudentForEnrollment> Students { get; set; } = new();
    }

    /// <summary>
    /// Данные для создания приказа (order)
    /// </summary>
    public class OrderCreateData
    {
        [JsonPropertyName("number")]
        public string Number { get; set; } = string.Empty;

        [JsonPropertyName("date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonPropertyName("type")]
        public OrderType Type { get; set; } = OrderType.enrollment;

        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;
    }

    /// <summary>
    /// Приказ с данными студентов (для создания)
    /// </summary>
    public class OrderWithStudents
    {
        [JsonPropertyName("order")]
        public Order Order { get; set; } = new();

        [JsonPropertyName("students")]
        public List<Student> Students { get; set; } = new();
    }
}
