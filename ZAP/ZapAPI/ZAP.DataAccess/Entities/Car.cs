using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ZAP.DataAccess.Entities
{
    public partial class Car
    {
        public Car()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int CarId { get; set; }
        public int CarClassId { get; set; }
        public int CarTypeId { get; set; }
        public int CarBrandId { get; set; }
        public string LicensePlate { get; set; }
        public int Odometer { get; set; }
        public DateTime ManufactureDate { get; set; }
        public int Price { get; set; }
        public byte[] Picture { get; set; }
        public string Name { get; set; }

        public virtual CarBrand CarBrand { get; set; }
        public virtual CarClass CarClass { get; set; }
        public virtual CarType CarType { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
