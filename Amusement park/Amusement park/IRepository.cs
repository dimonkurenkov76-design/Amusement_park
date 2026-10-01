namespace Amusement_park
{
    public interface IRepository
    {
        List<Zone> GetZones();
        List<Operator> GetOperators();
        List<Attraction> GetAttractions();
    }
}
