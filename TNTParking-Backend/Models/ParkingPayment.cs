namespace TNTParking_Backend.Models
{
    //public class ParkingPayment
    //{
    //    public bool HourPayment { get; set; }
    //    public DateTime Date { get; set; }
    //    public string DateString { get; set; }
    //    public string Email { get; set; }
    //    public string Telephone { get; set; }
    //    public string FromDate { get; set; }
    //    public string ToDate { get; set; }
    //    public decimal ParkingRate { get; set; }
    //}

    public class ParkingPaymentTariff
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string FromHour { get; set; }
        public string ToHour { get; set; }

        public ParkingPaymentTariff()
        {

        }

        public ParkingPaymentTariff(int id, decimal price, string fromHour, string toHour)
        {
            Id = id;
            Price = price;
            FromHour = fromHour;
            ToHour = toHour;
        }
    }
}
