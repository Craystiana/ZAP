using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ZAP.Common.Enums;
using ZAP.DataAccess.Entities;

namespace ZAP.DataAccess.Repositories
{
    public class ReservationRepository : Repository<Reservation>
    {
        public ReservationRepository(Context context) : base(context) { }

        public IEnumerable<Reservation> GetByUser(int userId)
        {
            return _context.Reservations.Include(r => r.Car)
                                        .ThenInclude(c => c.CarBrand)
                                        .Include(r => r.User)
                                        .Where(r => r.UserId == userId);
        }
        public IEnumerable<Reservation> GetByCar(int carId)
        {
            return _context.Reservations.Include(r => r.Car)
                                        .ThenInclude(c => c.CarBrand)
                                        .Include(r => r.User)
                                        .Where(r => r.CarId == carId);
        }
    }
}
