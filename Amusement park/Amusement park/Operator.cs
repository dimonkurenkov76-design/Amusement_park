using System;
/// <summary>
/// 
/// </summary>
public class Operator
{
    // Id доступен только для чтения снаружи класса
    /// <summary>
    /// Уникальный Индификатор
    /// </summary>
    public int Id { get; }
    /// <summary>
    /// Полное Имя
    /// </summary>
    public string FullName { get; set; } //     get { действия, выполняемые при получении значения свойства}
    /// <summary>
    /// Тип смены: Дневная или ночная
    /// </summary>
    public string Shift { get; set; } //     set { действия, выполняемые при установке значения свойства}
                                      //   init позволяет установить значение только во время создания объекта
    /// <summary>
    /// Опыт работы
    /// </summary>
    public int Experience { get; set; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Уникальный Индификатор</param>
    /// <param name="fullName">полное имя</param>
    /// <param name="shift">Тип смены: Дневная или ночная</param>
    /// <param name="experience">Опыт работы</param>
    /// <exception cref="ArgumentOutOfRangeException">Пробрасывает исключение, после его проверки</exception>
    /// <exception cref="ArgumentException">Пробрасывает исключение, если некорректное данные</exception>
    public Operator(int id, string fullName, string shift, int experience)
    {
        
        if (id < 1)
            throw new ArgumentOutOfRangeException("Id не может быть меньше 1");
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Имя оператора не может быть пустым");
        if (string.IsNullOrWhiteSpace(shift))
            throw new ArgumentException("Смена не может быть пустой");
        if (experience < 0)
            throw new ArgumentOutOfRangeException("Опыт работы не может быть отрицательным");
        
        Id = id; 
        FullName = fullName;
        Shift = shift;
        Experience = experience;
    }
    /// <summary>
    /// Проверяет на опытность сотрудника, опыт > 3 должен быть 
    /// </summary>
    public bool IsExperienced => Experience > 3;
    /// <summary>
    /// Собирает и выводит всю информацию по операторам
    /// </summary>
    /// <returns></returns>
    public string GetInfo() => $"{FullName} ({Experience} лет опыта)";
}
