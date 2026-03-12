using System.Text.Json.Serialization;

namespace UniversityContingent.Models
{
    /// <summary>
    /// Факультет
    /// </summary>
    public class Faculty
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

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
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("faculty_id")]
        public string FacultyId { get; set; } = string.Empty;
        
        [JsonPropertyName("study_duration_years")]
        public int StudyDurationYears { get; set; }

        [JsonPropertyName("education_form")]
        public string EducationFormRaw { get; set; } = string.Empty;
        
        [JsonIgnore]
        public EducationForm EducationForm => EducationFormRaw switch
        {
            "full_time" => EducationForm.full_time,
            "part_time" => EducationForm.part_time,
            "extramural" => EducationForm.extramural,
            _ => EducationForm.full_time
        };
    }

    /// <summary>
    /// Учебная группа
    /// </summary>
    public class Group
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("direction_id")]
        public string DirectionId { get; set; } = string.Empty;

        [JsonPropertyName("course")]
        public int Course { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }
    }

    /// <summary>
    /// Студент
    /// </summary>
    public class Student
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("birth_date")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("group_id")]
        public string GroupId { get; set; } = string.Empty;

        [JsonPropertyName("enrollment_date")]
        public DateTime EnrollmentDate { get; set; }

        [JsonPropertyName("status")]
        public StudentStatus Status { get; set; }

        [JsonPropertyName("enrollment_order_id")]
        public string? EnrollmentOrderId { get; set; }
    }

    /// <summary>
    /// Приказ
    /// </summary>
    public class Order
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public OrderType Type { get; set; }

        [JsonPropertyName("number")]
        public string? Number { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        [JsonPropertyName("student_ids")]
        public List<int> StudentIds { get; set; } = new();

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Специальный приказ с данными студентов
    /// </summary>
    public class SpecialOrder
    {
        [JsonPropertyName("order")]
        public Order Order { get; set; } = new();

        [JsonPropertyName("students")]
        public List<Student> Students { get; set; } = new();
    }

    /// <summary>
    /// Данные для зачисления студента
    /// </summary>
    public class EnrollmentStudent
    {
        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("birth_date")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("group_id")]
        public int GroupId { get; set; }
    }

    /// <summary>
    /// Данные для приказа о зачислении
    /// </summary>
    public class EnrollmentOrderData
    {
        [JsonPropertyName("students")]
        public List<EnrollmentStudent> Students { get; set; } = new();

        [JsonPropertyName("date")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
