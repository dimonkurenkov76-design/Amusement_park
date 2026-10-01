using System;

public class Zone
{
    // Убрали set, теперь Id можно установить только ОДИН раз — в конструкторе
    public int Id { get; }
    public string Name { get; set; }
    public double Area { get; set; }

    public Zone(int id, string name, double area)
    {
        if (id < 1)
            throw new ArgumentOutOfRangeException("Id не может быть меньше 1");
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя зоны не может быть пустым");
        if (area <= 0)
            throw new ArgumentOutOfRangeException("Площадь зоны должна быть больше 0");

        Id = id; // Это легально, конструктор имеет право заполнять свойства без set
        Name = name;
        Area = area;
    }

    public bool IsFamily => Name == "Семейная";

    public string GetInfo() => $"{Name} ({Area} м²)";
}
