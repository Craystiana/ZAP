using System;

namespace ZAP.BusinessLogic.DTO.Car
{
    public class CarEditModel
    {
        public int? CarId { get; set; }

        public int Class { get; set; }

        public int Type { get; set; }

        public int Brand { get; set; }

        public string LicensePlate { get; set; }

        public int Odometer { get; set; }

        public DateTime ManufactureDate { get; set; }

        public int Price { get; set; }

        public string Picture { get; set; }

        public string Name { get; set; }
    }
}
