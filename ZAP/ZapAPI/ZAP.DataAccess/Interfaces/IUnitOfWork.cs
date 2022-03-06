using System;
using ZAP.DataAccess.Entities;
using ZAP.DataAccess.Repositories;

namespace ZAP.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICarRepository CarRepository { get; }
        IRepository<CarBrand> CarBrandRepository { get; }
        IRepository<CarClass> CarClassRepository { get; }
        IRepository<CarType> CarTypeRepository { get; }
        ReservationRepository ReservationRepository { get; }
        IRepository<ReservationType> ReservationTypeRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<UserRole> UserRoleRepository { get; }
        

        void Dispose();

        int SaveChanges();
    }
}
