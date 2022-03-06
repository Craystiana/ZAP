using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZAP.Common.Enums;
using ZAP.DataAccess.Entities;

namespace ZAP.DataAccess.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly Context _context;

        public CarRepository(Context context)
        {
            _context = context;
        }

        public void Add(Car car)
        {
            _context.Cars.Add(car);
        }

        public void AddRange(IEnumerable<Car> cars)
        {
            _context.Cars.AddRange(cars);
        }

        public IEnumerable<Car> Find(Expression<Func<Car, bool>> predicate)
        {
            return _context.Cars.Where(predicate);
        }

        public IEnumerable<Car> GetAll()
        {
            return _context.Cars.ToList();
        }

        public Car Get(int carId)
        {
            return _context.Cars.Include(c => c.CarType)
                                .Include(c => c.CarClass)
                                .Include(c => c.CarBrand)
                                .FirstOrDefault(c => c.CarId == carId);
        }

        public IEnumerable<Car> GetList(string search,
                                        IEnumerable<int> carTypeIds,
                                        IEnumerable<int> carClassIds,
                                        IEnumerable<int> carBrandIds,
                                        int? sortById)
        {
            IQueryable<Car> cars = _context.Cars.Include(c => c.CarType)
                                                 .Include(c => c.CarBrand)
                                                 .Include(c => c.CarClass);

            if (!string.IsNullOrEmpty(search))
            {
                cars = cars.Where(c => c.Name.Contains(search));
            }

            if (carTypeIds != null)
            {
                cars = cars.Where(c => carTypeIds.Contains(c.CarTypeId));
            }

            if (carClassIds != null)
            {
                cars = cars.Where(c => carClassIds.Contains(c.CarClassId));
            }

            if (carBrandIds != null)
            {
                cars = cars.Where(c => carBrandIds.Contains(c.CarBrandId));
            }

            if (sortById != null)
            {
                switch (sortById)
                {
                    case (int)SortType.Name:
                        cars = cars.OrderBy(c => c.Name);
                        break;

                    case (int)SortType.Price:
                        cars = cars.OrderBy(c => c.Price);
                        break;

                    case (int)SortType.Odometer:
                        cars = cars.OrderBy(c => c.Odometer);
                        break;

                    case (int)SortType.ManufactureDate:
                        cars = cars.OrderBy(c => c.ManufactureDate);
                        break;
                }
            }

            return cars;
        }

        public void Remove(Car car)
        {
            _context.Cars.Remove(car);
        }

        public void RemoveRange(IEnumerable<Car> cars)
        {
            _context.Cars.RemoveRange(cars);
        }

        public Car SingleOrDefault(Expression<Func<Car, bool>> predicate)
        {
            return _context.Cars.SingleOrDefault(predicate);
        }
    }
}
