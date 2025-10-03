using TNTParking_Backend.Interfaces.Settings;

namespace TNTParking_Backend.Models
{
    public class ConnectionString : IDatabaseSettings
    {
        public string TntEcoIslandsDB { get; set; }
    }
}
