using Amusement_park;

public class InMemoryRepository : IRepository
{
    public List<Zone> GetZones() => new List<Zone>
    {
        new Zone(1, "Семейная", 500),
        new Zone(2, "Экстрим", 300)
    };

    public List<Operator> GetOperators() => new List<Operator>
    {
        new Operator(1, "Сидоров С.С.", "Дневная", 5),
        new Operator(2, "Петров П.П.", "Ночная", 2)
    };

    public List<Attraction> GetAttractions() => new List<Attraction>
    {
        new Attraction(1, "Колесо обозрения", 1, 1, 300, 50),
        new Attraction(2, "Американские горки", 2, 2, 500, 30)
    };
}
