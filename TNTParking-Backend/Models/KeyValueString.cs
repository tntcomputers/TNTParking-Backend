namespace TNTParking_Backend.Models
{
    public class KeyValueString
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public KeyValueString()
        {

        }

        public KeyValueString(int key, string value)
        {
            this.Id = key;
            this.Value = value;
        }
    }
}
