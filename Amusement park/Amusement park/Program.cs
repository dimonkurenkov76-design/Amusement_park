namespace Amusement_park
{
    internal class Program
    {
        private static IRepository _repository;
        static void Main(string[] args)

        {            
            try
            {
                Console.WriteLine("Выберите от куда загружаем 1: InMemoryRepository или 2: CsvRepository");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int number))
                {
                    Console.WriteLine("Введите корректное число");
                    return;
                }

                switch (number)
                {
                    case 1:
                        _repository = new InMemoryRepository();
                        break;
                    case 2:
                        _repository = new CsvRepository("data.csv");
                        break;
                    default: Console.WriteLine("Неверный выбор"); return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при считывание или инициализаций{ex.Message}");
                return;
            }
            if(_repository == null)
            {
                Console.WriteLine("Репозиторий не инициализирован");
                return;
            }
            //проверяем на наличие данных в репозиторий 
            List<Attraction> allAttractions = _repository.GetAttractions(); 
            if (allAttractions == null || allAttractions.Count == 0)
            {
                Console.WriteLine("Репозиторий пуст");
                return;
            }
            Console.WriteLine("\n --- Результат ---");
            
            Operator op = FinOperator("Колесо обозрения");
            Console.WriteLine($"1. FindOperator(\"Колесо обозрения\"):{(op != null ? op.GetInfo() : "null")}");

            Zone zo = FindZone("Колесо обозрения");
            Console.WriteLine($"2. FindZone(attraction \"Колесо обозрения\"):{(zo != null ? zo.GetInfo() : "null")}");

            Console.WriteLine($"3. GetTotalCapacity: {GetTotalCapacity()} человек");

            Zone maxZone = GetZoneWithMaxCapacity();
            Console.WriteLine($"4. GetZoneWithMaxCapacity: {(maxZone != null ? maxZone.GetInfo() : "null")}");

            Console.WriteLine("5.PrintAllAttractions:");
            PrintAllAttractions();

            Console.WriteLine("\n --- Тест на отсутстви данных ---");
            Operator unknownOp = FinOperator("Неизвестный аттракцион");
            Console.WriteLine($"FindOperator(\"Неизвестный аттракцион\") -> {(unknownOp != null ? unknownOp.GetInfo() : "null")}");
        }
        public static Operator FinOperator(string attractionName)
        {
            if (_repository == null) return null;
            List<Attraction> attractions =  _repository.GetAttractions();
            if (attractions == null) return null;

            Attraction targetAttraction = null;
            foreach (Attraction a in attractions)
            {
                if(a != null && a.Name == attractionName)
                {
                    targetAttraction = a;
                    break;
                }
            }
            if (targetAttraction == null) return null;
            List<Operator> operators = _repository.GetOperators();
            foreach (Operator op in operators)
            {
                if (op != null && op.Id == targetAttraction.OperatorId) return op;
            }
            return null;
        }
        public static Zone FindZone(string attractionName)
        {
            if (_repository == null) return null;
            List<Attraction> attractions= _repository.GetAttractions();
            if (attractions == null) return null;

            Attraction targetAttraction = null;
            foreach (Attraction a in attractions)
            {
                if (a != null && a.Name == attractionName)
                {
                    targetAttraction = a;
                    break;
                }
            }
            if(targetAttraction == null) return null;
            List<Zone> zones = _repository.GetZones();
            foreach (Zone z in zones)
            {
                if (z != null && z.Id == targetAttraction.ZoneId) return z;
            }
            return null;
        }
        public static int GetTotalCapacity()
        {
            if(_repository==null) return 0;
            List<Attraction> attractions = _repository.GetAttractions();
            if(attractions == null || attractions.Count == 0) return 0;

            int total = 0;
            foreach (Attraction a in attractions)
            {
                if(a != null) total += a.Capacity;
            }
            return total;
    
        }
        public static Zone GetZoneWithMaxCapacity()
        {
            if (_repository == null) return null;
            List<Attraction> attractions = _repository.GetAttractions();
            List<Zone> zones = _repository.GetZones();

            if (attractions == null || attractions.Count == 0 || zones == null || zones.Count == 0)
                return null;
            Zone maxZone = null;
            int maxCapacity = -1;
            foreach(Zone z in zones)
            {
                if (z == null) continue;
                
                int currentZoneCapacity = 0;
                bool hasAttraction = false;

                foreach(Attraction a in attractions)
                {
                    if (a != null && a.ZoneId == z.Id)
                    {
                        currentZoneCapacity += a.Capacity;
                        hasAttraction = true;
                    } 

                }
                if(hasAttraction && currentZoneCapacity > maxCapacity)
                {
                    maxCapacity = currentZoneCapacity;
                    maxZone = z;
                }
            }
            return maxZone;
        }
        public static void PrintAllAttractions()
        {
            if (_repository == null) { Console.WriteLine("-"); return; }
            List<Attraction> attractions = _repository.GetAttractions();
            if(attractions == null || attractions.Count == 0) {  Console.WriteLine("-"); return; }
            
            List<Operator> operators = _repository.GetOperators();
            List<Zone> zones = _repository.GetZones();

            foreach (Attraction attraction in attractions)
            {
                if (attraction == null) continue;
                string opName = "-";
                string zoneName = "-";

                if (operators != null)
                {
                    foreach (Operator op in operators)
                    {
                        if (op != null && op.Id == attraction.OperatorId)
                        {
                            opName = op.FullName; break;
                        }
                    }
                }
                if (zones != null)
                {
                    foreach (Zone z in zones)
                    {
                        if (z != null && z.Id == attraction.ZoneId) { zoneName = z.Name; break; }
                    }
                }
            Console.WriteLine($"\"{attraction.GetInfo()}\" - оператор {opName}, зона \"{zoneName}\"");
            }
        }
    }
}
