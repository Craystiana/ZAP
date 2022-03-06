using System;

namespace ZAP.BusinessLogic.DTO.Reservation
{
    public class ReservationModel
    {
        public int CarId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
