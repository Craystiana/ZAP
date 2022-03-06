using ZAP.DataAccess.Entities;
using ZAP.DataAccess.Interfaces;
using ZAP.DataAccess.Repositories;

namespace ZAP.UnitTests.TestRepositories
{
    public class TestUnitOfWork : IUnitOfWork
    {
        private ICarRepository _carRepository;
        private IRepository<User> _userRepository;

        public ICarRepository CarRepository => _carRepository ??= new TestCarRepository();

        public IRepository<CarBrand> CarBrandRepository => throw new System.NotImplementedException();

        public IRepository<CarClass> CarClassRepository => throw new System.NotImplementedException();

        public IRepository<CarType> CarTypeRepository => throw new System.NotImplementedException();

        public ReservationRepository ReservationRepository => throw new System.NotImplementedException();

        public IRepository<ReservationType> ReservationTypeRepository => throw new System.NotImplementedException();

        public IRepository<User> UserRepository => _userRepository ??= new TestUserRepository();

        public IRepository<UserRole> UserRoleRepository => throw new System.NotImplementedException();


        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
