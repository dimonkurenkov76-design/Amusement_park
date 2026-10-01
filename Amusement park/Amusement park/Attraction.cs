using System;

public class Attraction
{
    // Убрали модификатор set
    public int Id { get; }
    public string Name { get; set; }
    public int ZoneId { get; set; }
    public int OperatorId { get; set; }
    public decimal Price { get; set; }
    public int Capacity { get; set; }

    public Attraction(int id, string name, int zoneId, int operatorId, decimal price, int capacity)
    {
        if (id < 1)
            throw new ArgumentOutOfRangeException("Id не может быть меньше 1");
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название аттракциона не может быть пустым");
        if (zoneId < 1)
            throw new ArgumentOutOfRangeException("ZoneId не может быть меньше 1");
        if (operatorId < 1)
            throw new ArgumentOutOfRangeException("OperatorId не может быть меньше 1");
        if (price < 0)
            throw new ArgumentOutOfRangeException("Цена не может быть отрицательной");
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException("Вместимость должна быть больше 0");

        Id = id; // Успешно инициализируем read-only свойство
        Name = name;
        ZoneId = zoneId;
        OperatorId = operatorId;
        Price = price;
        Capacity = capacity;
    }

    public bool IsExtreme => Name != null && Name.Contains("Экстрим");

    public decimal GetTotalRevenue(int visitors)
    {
        if (visitors <= 0) return 0;
        return Price * visitors;
    }

    public string GetInfo() => $"{Name} ({Price} руб., {Capacity} мест)";
}
