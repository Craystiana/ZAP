using Microsoft.EntityFrameworkCore;
using System;
using ZAP.DataAccess.Entities;
using ZAP.DataAccess.Interfaces;
using ZAP.DataAccess.Repositories;

namespace ZAP.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        private bool isDisposed;

        public ICarRepository CarRepository => new CarRepository(_context);

        public IRepository<CarBrand> CarBrandRepository => new Repository<CarBrand>(_context);

        public IRepository<CarClass> CarClassRepository => new Repository<CarClass>(_context);

        public IRepository<CarType> CarTypeRepository => new Repository<CarType>(_context);

        public ReservationRepository ReservationRepository => new ReservationRepository(_context);

        public IRepository<ReservationType> ReservationTypeRepository => new Repository<ReservationType>(_context);

        public IRepository<User> UserRepository => new Repository<User>(_context);

        public IRepository<UserRole> UserRoleRepository => new Repository<UserRole>(_context);

        public UnitOfWork(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);

            _context = new Context(optionsBuilder.Options);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
