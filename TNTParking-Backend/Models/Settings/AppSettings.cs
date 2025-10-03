namespace TNTParking_Backend.Models.Settings
{
    public class AppSettingsTNTParking
    {
        public string DatabaseProvider { get; set; }
        public string ConnectionStringTNTAuthenticatorPG { get; set; }
        public string ConnectionStringTNTAuthenticatorMS { get; set; }
        public string ConnectionStringTNTParkingsPG { get; set; }
        public string LOGConnectionStringMongo { get; set; }
        public string HISTORYConnectionStringMongo { get; set; }
        public string LOGDataBaseName { get; set; }
        public string HISTTORYDataBaseNameMongo { get; set; }
        public string LOGColectionName { get; set; }
        public string HISTTORYColectionName { get; set; }
        public string ApiKeySendGrid { get; set; }
        public string InstrumentationKey { get; set; }
        public string StartManagerBaseUrl { get; set; }
        public JWTEcoIsland JWT { get; set; }
        public CameraIpConfiguration CameraIpConfiguration { get; set; }
        public string UnitSecret { get; set; }

    }

    //public class DatabaseSettingsMongo
    //{
    //    public string ConnectionStringMongo { get; set; }
    //    public string DatabaseNameMongo { get; set; }
    //}

    public class CameraIpConfiguration
    {
        public string camera_port { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class JWTEcoIsland
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int ExpireDays { get; set; }
        public string Audience { get; set; }
        public string[] AllowedOrigins { get; set; }
    }
}
