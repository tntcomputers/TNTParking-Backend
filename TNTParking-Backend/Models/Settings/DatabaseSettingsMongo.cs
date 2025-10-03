using TNTParking_Backend.Interfaces.Settings;

namespace TNTParking_Backend.Models.Settings
{
    public class DatabaseSettingsMongo : IDatabaseSettingsMongo
    {
        public string ConnectionStringMongo { get; set; }
        public string DatabaseNameMongo { get; set; }
    }
}
