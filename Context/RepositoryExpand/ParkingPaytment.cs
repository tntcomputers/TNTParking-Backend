using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class ParkingPayment
    {
        public int Id { get; set; }

        public int UnitId { get; set; }

        public string Status { get; set; }

        public int IdAreaType { get; set; }

        public int IdArea { get; set; }

        public string CarPlate { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string StartDateStr { get; set; }
        public string EndDateStr { get; set; }
        public Area? Area { get; set; }

        public AreaTypes? AreaType { get; set; }

        public ParkingPayment()
        {

        }
        public ParkingPayment(ParkingParkingPayment parkingPayment)
        {
            this.Id = parkingPayment.Id;
            this.UnitId = parkingPayment.UnitId;
            this.Status = parkingPayment.Status;
            this.IdAreaType = parkingPayment.IdAreaType;
            this.IdArea = parkingPayment.IdArea;
            this.CarPlate = parkingPayment.CarPlate;
            this.Telephone = parkingPayment.Telephone;
            this.Email = parkingPayment.Email;
            this.StartDate = parkingPayment.StartDate;
            this.EndDate = parkingPayment.EndDate;
            this.StartDateStr = parkingPayment.StartDate.ToString("dd.MM.yyyy HH:mm");
            this.EndDateStr = parkingPayment.EndDate.ToString("dd.MM.yyyy HH:mm");
            this.Area = parkingPayment.IdAreaNavigation != null ? new Area(parkingPayment.IdAreaNavigation) : new Area();
            this.AreaType = parkingPayment.IdAreaTypeNavigation != null ? new AreaTypes(parkingPayment.IdAreaTypeNavigation) : new AreaTypes();
        }
    }
    public partial class ParkingParkingPayment
    {
        public void Map(ParkingPayment parkingPayment)
        {
            this.Id = parkingPayment.Id;
            this.UnitId = parkingPayment.UnitId;
            this.Status = parkingPayment.Status;
            this.IdAreaType = parkingPayment.IdAreaType;
            this.IdArea = parkingPayment.IdArea;
            this.CarPlate = parkingPayment.CarPlate;
            this.Telephone = parkingPayment.Telephone;
            this.Email = parkingPayment.Email;
            this.StartDate = Convert.ToDateTime(parkingPayment.StartDateStr, CultureInfo.InvariantCulture); //AreasDaysOff.StartDate;
            this.EndDate = Convert.ToDateTime(parkingPayment.EndDateStr, CultureInfo.InvariantCulture); //AreasDaysOff.EndDate;
        }
    }
}
