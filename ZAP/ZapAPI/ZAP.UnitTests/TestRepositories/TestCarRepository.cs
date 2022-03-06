using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZAP.DataAccess.Entities;
using ZAP.DataAccess.Repositories;

using Enum = ZAP.Common.Enums;

namespace ZAP.UnitTests.TestRepositories
{
    public class TestCarRepository : ICarRepository
    {
        private readonly List<Car> _cars;

        public TestCarRepository()
        {
            _cars = new List<Car>();

            _cars.Add(
                new Car
                {
                    CarId = 1,
                    CarBrandId = (int)Enum.CarBrand.Acura,
                    CarBrand = new CarBrand
                    {
                        CarBrandId = (int)Enum.CarBrand.Acura,
                        Name = Enum.CarBrand.Acura.ToString()
                    },
                    CarTypeId = (int)Enum.CarType.Coupe,
                    CarType = new CarType
                    {
                        CarTypeId = (int)Enum.CarType.Coupe,
                        Name = Enum.CarType.Coupe.ToString(),
                    },
                    CarClassId = (int)Enum.CarClass.Premium,
                    CarClass = new CarClass
                    {
                        CarClassId = (int)Enum.CarClass.Premium,
                        Name = Enum.CarClass.Premium.ToString()
                    },
                    LicensePlate = "Test Acura 1",
                    ManufactureDate = new DateTime(2015, 1, 15),
                    Name = "Acura 1",
                    Odometer = 15121,
                    Price = 150
                });
            _cars.Add(
                new Car
                {
                    CarId = 2,
                    CarBrandId = (int)Enum.CarBrand.AlfaRomeo,
                    CarBrand = new CarBrand
                    {
                        CarBrandId = (int)Enum.CarBrand.AlfaRomeo,
                        Name = Enum.CarBrand.AlfaRomeo.ToString()
                    },
                    CarTypeId = (int)Enum.CarType.Coupe,
                    CarType = new CarType
                    {
                        CarTypeId = (int)Enum.CarType.Coupe,
                        Name = Enum.CarType.Coupe.ToString(),
                    },
                    CarClassId = (int)Enum.CarClass.Premium,
                    CarClass = new CarClass
                    {
                        CarClassId = (int)Enum.CarClass.Premium,
                        Name = Enum.CarClass.Premium.ToString()
                    },
                    LicensePlate = "Test Alfa Romeo 1",
                    ManufactureDate = new DateTime(2015, 1, 15),
                    Name = "Alfa Romeo 1",
                    Odometer = 10,
                    Price = 5000
                });
            _cars.Add(
                new Car
                {
                    CarId = 3,
                    CarBrandId = (int)Enum.CarBrand.AlfaRomeo,
                    CarBrand = new CarBrand
                    {
                        CarBrandId = (int)Enum.CarBrand.AlfaRomeo,
                        Name = Enum.CarBrand.AlfaRomeo.ToString()
                    },
                    CarTypeId = (int)Enum.CarType.Coupe,
                    CarType = new CarType
                    {
                        CarTypeId = (int)Enum.CarType.Coupe,
                        Name = Enum.CarType.Coupe.ToString(),
                    },
                    CarClassId = (int)Enum.CarClass.Premium,
                    CarClass = new CarClass
                    {
                        CarClassId = (int)Enum.CarClass.Premium,
                        Name = Enum.CarClass.Premium.ToString()
                    },
                    LicensePlate = "Test Alfa Romeo 1",
                    ManufactureDate = new DateTime(2021, 1, 1),
                    Name = "Alfa Romeo 1",
                    Odometer = 10,
                    Price = 5000
                });
            _cars.Add(
                new Car
                {
                    CarId = 4,
                    CarBrandId = (int)Enum.CarBrand.AstonMartin,
                    CarBrand = new CarBrand
                    {
                        CarBrandId = (int)Enum.CarBrand.AstonMartin,
                        Name = Enum.CarBrand.AstonMartin.ToString()
                    },
                    CarTypeId = (int)Enum.CarType.Roadster,
                    CarType = new CarType
                    {
                        CarTypeId = (int)Enum.CarType.Roadster,
                        Name = Enum.CarType.Roadster.ToString(),
                    },
                    CarClassId = (int)Enum.CarClass.Large,
                    CarClass = new CarClass
                    {
                        CarClassId = (int)Enum.CarClass.Large,
                        Name = Enum.CarClass.Large.ToString()
                    },
                    LicensePlate = "Test Aston 1",
                    ManufactureDate = new DateTime(2018, 2, 1),
                    Name = "Aston Martin 1",
                    Odometer = 31212,
                    Price = 300
                });
            _cars.Add(
                new Car
                {
                    CarId = 5,
                    CarBrandId = (int)Enum.CarBrand.AstonMartin,
                    CarBrand = new CarBrand
                    {
                        CarBrandId = (int)Enum.CarBrand.AstonMartin,
                        Name = Enum.CarBrand.AstonMartin.ToString()
                    },
                    CarTypeId = (int)Enum.CarType.Roadster,
                    CarType = new CarType
                    {
                        CarTypeId = (int)Enum.CarType.Roadster,
                        Name = Enum.CarType.Roadster.ToString(),
                    },
                    CarClassId = (int)Enum.CarClass.Large,
                    CarClass = new CarClass
                    {
                        CarClassId = (int)Enum.CarClass.Large,
                        Name = Enum.CarClass.Large.ToString()
                    },
                    LicensePlate = "Test Aston 2",
                    ManufactureDate = new DateTime(2018, 6, 29),
                    Name = "Aston Martin 2",
                    Odometer = 10000,
                    Price = 350
                });
            _cars.Add(
                new Car
                {
                    CarId = 6,
                    CarBrandId = (int)Enum.CarBrand.Audi,
                    CarBrand = new CarBrand
                    {
                        CarBrandId = (int)Enum.CarBrand.Audi,
                        Name = Enum.CarBrand.Audi.ToString()
                    },
                    CarTypeId = (int)Enum.CarType.Limousine,
                    CarType = new CarType
                    {
                        CarTypeId = (int)Enum.CarType.Limousine,
                        Name = Enum.CarType.Limousine.ToString(),
                    },
                    CarClassId = (int)Enum.CarClass.Large,
                    CarClass = new CarClass
                    {
                        CarClassId = (int)Enum.CarClass.Large,
                        Name = Enum.CarClass.Large.ToString()
                    },
                    LicensePlate = "Test Audi 1",
                    ManufactureDate = new DateTime(2021, 1, 29),
                    Name = "Audi RS6",
                    Odometer = 200,
                    Price = 6000
                });
            _cars.Add(
                new Car
                {
                    CarId = 7,
                    CarBrandId = (int)Enum.CarBrand.Audi,
                    CarBrand = new CarBrand
                    {
                        CarBrandId = (int)Enum.CarBrand.Audi,
                        Name = Enum.CarBrand.Audi.ToString()
                    },
                    CarTypeId = (int)Enum.CarType.Limousine,
                    CarType = new CarType
                    {
                        CarTypeId = (int)Enum.CarType.Limousine,
                        Name = Enum.CarType.Limousine.ToString(),
                    },
                    CarClassId = (int)Enum.CarClass.Large,
                    CarClass = new CarClass
                    {
                        CarClassId = (int)Enum.CarClass.Large,
                        Name = Enum.CarClass.Large.ToString()
                    },
                    LicensePlate = "Test Audi 2",
                    ManufactureDate = new DateTime(2019, 1, 3),
                    Name = "Audi RS7",
                    Odometer = 151021,
                    Price = 2000
                });
            _cars.Add(
                new Car
                {
                    CarId = 8,
                    CarBrandId = (int)Enum.CarBrand.Ford,
                    CarBrand = new CarBrand
                    {
                        CarBrandId = (int)Enum.CarBrand.Ford,
                        Name = Enum.CarBrand.Ford.ToString()
                    },
                    CarTypeId = (int)Enum.CarType.SUV,
                    CarType = new CarType
                    {
                        CarTypeId = (int)Enum.CarType.SUV,
                        Name = Enum.CarType.SUV.ToString(),
                    },
                    CarClassId = (int)Enum.CarClass.Large,
                    CarClass = new CarClass
                    {
                        CarClassId = (int)Enum.CarClass.Large,
                        Name = Enum.CarClass.Large.ToString()
                    },
                    LicensePlate = "Test Ford 1",
                    ManufactureDate = new DateTime(2019, 1, 3),
                    Name = "Ford F150",
                    Odometer = 31231,
                    Price = 400
                });
            _cars.Add(
                new Car
                {
                    CarId = 9,
                    CarBrandId = (int)Enum.CarBrand.Ford,
                    CarBrand = new CarBrand
                    {
                        CarBrandId = (int)Enum.CarBrand.Ford,
                        Name = Enum.CarBrand.Ford.ToString()
                    },
                    CarTypeId = (int)Enum.CarType.Coupe,
                    CarType = new CarType
                    {
                        CarTypeId = (int)Enum.CarType.Coupe,
                        Name = Enum.CarType.Coupe.ToString(),
                    },
                    CarClassId = (int)Enum.CarClass.Medium,
                    CarClass = new CarClass
                    {
                        CarClassId = (int)Enum.CarClass.Medium,
                        Name = Enum.CarClass.Medium.ToString()
                    },
                    LicensePlate = "Test Ford 2",
                    ManufactureDate = new DateTime(2019, 12, 30),
                    Name = "Ford Focus",
                    Odometer = 41212,
                    Price = 8921
                });
            _cars.Add(
                new Car
                {
                    CarId = 10,
                    CarBrandId = (int)Enum.CarBrand.BMW,
                    CarBrand = new CarBrand
                    {
                        CarBrandId = (int)Enum.CarBrand.BMW,
                        Name = Enum.CarBrand.BMW.ToString()
                    },
                    CarTypeId = (int)Enum.CarType.Coupe,
                    CarType = new CarType
                    {
                        CarTypeId = (int)Enum.CarType.Coupe,
                        Name = Enum.CarType.Coupe.ToString(),
                    },
                    CarClassId = (int)Enum.CarClass.Medium,
                    CarClass = new CarClass
                    {
                        CarClassId = (int)Enum.CarClass.Medium,
                        Name = Enum.CarClass.Medium.ToString()
                    },
                    LicensePlate = "Test BMW 1",
                    ManufactureDate = new DateTime(2016, 5, 30),
                    Name = "BMW M4",
                    Odometer = 2131,
                    Price = 24121
                });
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void AddRange(IEnumerable<Car> cars)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> Find(Expression<Func<Car, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Car Get(int id)
        {
            return _cars.FirstOrDefault(c => c.CarId == id);
        }

        public IEnumerable<Car> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> GetList(string search, IEnumerable<int> carTypeIds, IEnumerable<int> carClassIds, IEnumerable<int> carBrandIds, int? sortById)
        {
            IEnumerable<Car> cars = _cars;

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
                    case (int)Enum.SortType.Name:
                        cars = cars.OrderBy(c => c.Name);
                        break;

                    case (int)Enum.SortType.Price:
                        cars = cars.OrderBy(c => c.Price);
                        break;

                    case (int)Enum.SortType.Odometer:
                        cars = cars.OrderBy(c => c.Odometer);
                        break;

                    case (int)Enum.SortType.ManufactureDate:
                        cars = cars.OrderBy(c => c.ManufactureDate);
                        break;
                }
            }

            return cars;
        }

        public void Remove(Car car)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Car> entities)
        {
            throw new NotImplementedException();
        }

        public Car SingleOrDefault(Expression<Func<Car, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
