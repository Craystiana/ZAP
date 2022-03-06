using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ZAP.DataAccess.Entities
{
    public partial class ReservationType
    {
        public ReservationType()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int ReservationTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
