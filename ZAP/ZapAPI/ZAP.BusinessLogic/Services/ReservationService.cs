using System;
using System.Collections.Generic;
using System.Linq;
using ZAP.BusinessLogic.DTO.Generic;
using ZAP.BusinessLogic.DTO.Reservation;
using ZAP.DataAccess.Entities;
using ZAP.DataAccess.Interfaces;
using Enum = ZAP.Common.Enums;

namespace ZAP.BusinessLogic.Services
{
    public class ReservationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsBlacklisted(int userId)
        {
            return _unitOfWork.UserRepository.Get(userId).IsBlacklisted;
        }
        
        public bool CanReserve(int carId, DateTime startDate, DateTime endDate)
        {
            var reservations = _unitOfWork.ReservationRepository.Find(r => r.CarId == carId &&
                                                                          (r.StartDate < startDate && startDate < r.EndDate) ||
                                                                          (r.StartDate < endDate && endDate < r.EndDate));
            return reservations.Any();
        }

        public bool HasAccess(int reservationId, int userId)
        {
            return _unitOfWork.ReservationRepository.Get(reservationId).UserId == userId;
        }

        public void AddReservation(ReservationModel model, int userId, bool isAdmin)
        {
            _unitOfWork.ReservationRepository.Add(new Reservation
            {
                CarId = model.CarId,
                ReservationTypeId = isAdmin ? (int)Enum.ReservationType.Maintenance : (int)Enum.ReservationType.Basic,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                UserId = userId
            });
        }

        public void RemoveReservation(int reservationId)
        {
            var reservation = _unitOfWork.ReservationRepository.Get(reservationId);

            _unitOfWork.ReservationRepository.Remove(reservation);
        }

        public IEnumerable<ReservationDetailModel> GetUserReservations(int userId)
        {
            return _unitOfWork.ReservationRepository.GetByUser(userId)
                                                    .Select(r => new ReservationDetailModel
                                                    {
                                                        ReservationId = r.ReservationId,
                                                        ReservationTypeId = r.ReservationTypeId,
                                                        CarId = r.CarId,
                                                        CarName = r.Car.CarBrand.Name + " " + r.Car.Name,
                                                        UserName = r.User.LastName + " " + r.User.FirstName,
                                                        StartDate = r.StartDate,
                                                        EndDate = r.EndDate,
                                                        Incident = r.Incident
                                                    });

        }

        public IEnumerable<ReservationDetailModel> GetCarReservations(int carId)
        {
            return _unitOfWork.ReservationRepository.GetByCar(carId)
                                                    .Select(r => new ReservationDetailModel
                                                    {
                                                        ReservationId = r.ReservationId,
                                                        ReservationTypeId = r.ReservationTypeId,
                                                        CarId = r.CarId,
                                                        CarName = r.Car.CarBrand.Name + " " + r.Car.Name,
                                                        UserId = r.UserId,
                                                        UserName = r.User.LastName + " " + r.User.FirstName,
                                                        StartDate = r.StartDate,
                                                        EndDate = r.EndDate,
                                                        Incident = r.Incident
                                                    });
        }
    }
}
