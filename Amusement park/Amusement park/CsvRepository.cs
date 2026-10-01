using Amusement_park;
public class CsvRepository : IRepository
{
    private string _path;
    private List<Zone> _zones;
    private List<Operator> _operators;
    private List<Attraction> _attractions;

    public CsvRepository(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Путь к файлу не может быть пустым");

        _path = path;
        _zones = new List<Zone>();
        _operators = new List<Operator>();
        _attractions = new List<Attraction>();

        LoadDataFromFile();
    }
    //Реализация методов интерфейса
    public List<Zone> GetZones() => _zones;
    public List<Operator> GetOperators() => _operators;
    public List<Attraction> GetAttractions() => _attractions;
    //Главный метод по чтению файла 
    private void LoadDataFromFile()
    {
        if (!File.Exists(_path))
            throw new FileNotFoundException($"Файл базы данных не найден по пути: {_path}");

        using (StreamReader reader = new StreamReader(_path))
        {
            string currentSection = "";
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string trimmedLine = line.Trim();
                if (trimmedLine == "[Zones]" || trimmedLine == "[Operators]" || trimmedLine == "[Attractions]")
                {
                    currentSection = trimmedLine;
                    continue;
                }

                if (trimmedLine.StartsWith("Id;") || trimmedLine.StartsWith("Id"))
                {
                    continue;
                }

                string[] parts = trimmedLine.Split(';');
                if (currentSection == "[Zones]")
                {
                    ParseZone(parts);
                }
                else if (currentSection == "[Operators]")
                {
                    ParseOperator(parts);
                }
                else if (currentSection == "[Attractions]")
                {
                    ParseAttraction(parts);
                }
            }
        }
    }

    private void ParseZone(string[] parts)
    {
        if (parts == null || parts.Length < 3) return;
        if (int.TryParse(parts[0], out int id) &&
            double.TryParse(parts[2], out double area))
        {
            string name = parts[1];
            _zones.Add(new Zone(id, name, area));
        }
    }

    private void ParseOperator(string[] parts)
    {
        if (parts == null || parts.Length < 4) return;
        if (int.TryParse(parts[0], out int id) &&
            int.TryParse(parts[3], out int experience))
        {
            string fullName = parts[1];
            string shift = parts[2];
            _operators.Add(new Operator(id, fullName, shift, experience));
        }
    }

    private void ParseAttraction(string[] parts)
    {
        if (parts == null || parts.Length < 6) return;

        if (int.TryParse(parts[0], out int id) &&
            int.TryParse(parts[2], out int zoneId) &&
            int.TryParse(parts[3], out int operatorId) &&
            decimal.TryParse(parts[4], out decimal price) &&
            int.TryParse(parts[5], out int capacity))
        {
            string name = parts[1];
            _attractions.Add(new Attraction(id, name, zoneId, operatorId, price, capacity));
        }
    }
}
