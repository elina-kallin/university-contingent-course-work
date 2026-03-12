namespace UniversityContingent.Models
{
    /// <summary>
    /// Статус студента
    /// </summary>
    public enum StudentStatus
    {
        study,              // Обучается
        expelled,           // Отчислен
        academic_leave      // Академический отпуск
    }

    /// <summary>
    /// Тип приказа
    /// </summary>
    public enum OrderType
    {
        enrollment,         // Зачисление
        expulsion,          // Отчисление
        next_course,        // Перевод на следующий курс
        academic_leave,     // Предоставление академического отпуска
        transfer_direction  // Перевод на другое направление
    }

    /// <summary>
    /// Причина отчисления
    /// </summary>
    public enum ExpulsionReason
    {
        debts,          // Академическая задолженность
        health,         // По состоянию здоровья
        personal_reason,// Личные обстоятельства
        own             // По собственному желанию
    }

    /// <summary>
    /// Причина академического отпуска
    /// </summary>
    public enum AcademicLeaveReason
    {
        health,         // По состоянию здоровья
        personal_family // По личным/семейным обстоятельствам
    }

    /// <summary>
    /// Тип контроля
    /// </summary>
    public enum ControlType
    {
        test,           // Зачёт
        exam,           // Экзамен
        course_work     // Курсовая работа
    }

    /// <summary>
    /// Форма обучения
    /// </summary>
    public enum EducationForm
    {
        full_time,      // Очная
        part_time,      // Заочная
        extramural      // Очно-заочная
    }
}
