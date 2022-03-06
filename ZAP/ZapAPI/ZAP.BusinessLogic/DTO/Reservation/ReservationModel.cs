namespace ZAP.BusinessLogic.DTO.Reservation
{
    public class ReservationDetailModel : ReservationModel
    {
        public int ReservationId { get; set; }

        public int ReservationTypeId { get; set; }

        public string CarName { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public bool Incident { get; set; }
    }
}
