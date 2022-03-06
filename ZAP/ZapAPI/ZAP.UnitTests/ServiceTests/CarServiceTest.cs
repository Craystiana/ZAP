using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZAP.BusinessLogic.DTO.Car;
using ZAP.BusinessLogic.Services;
using ZAP.DataAccess;
using ZAP.DataAccess.Entities;
using ZAP.DataAccess.Interfaces;
using ZAP.DataAccess.Repositories;
using ZAP.UnitTests.TestRepositories;

using Enums = ZAP.Common.Enums;

namespace ZAP.UnitTests.ServiceTests
{
    [TestClass]
    public class CarServiceTest
    {
        private IUnitOfWork _unitOfWork;
        private CarService _carService;

        [TestInitialize]
        public void InitializeData()
        {
            _unitOfWork = new TestUnitOfWork();
            _carService = new CarService(_unitOfWork);
        }

        [TestMethod]
        public void GetCar_ReturnCorrectCar()
        {
            var carId = 3;
            var car = _carService.GetCar(carId);

            Assert.IsTrue(car.CarId == carId);
        }

        [TestMethod]
        public void CarSearch_ReturnOnlyGivenCarBrand()
        {
            var carBrandList = (int[]) Enum.GetValues(typeof(Enums.CarBrand));
            
            foreach(var carBrand in carBrandList)
            {
                var carQuery = new CarQueryModel
                {
                    CarBrandIds = new List<int> { carBrand }
                };
                var cars = _carService.GetList(carQuery);

                var result = cars.Any(c => c.Brand != ((Enums.CarBrand)carBrand).ToString());

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void CarSearch_ReturnOnlyGivenCarType()
        {
            var carTypeList = (int[])Enum.GetValues(typeof(Enums.CarType));

            foreach (var carType in carTypeList)
            {
                var carQuery = new CarQueryModel
                {
                    CarTypeIds = new List<int> { carType }
                };
                var cars = _carService.GetList(carQuery);

                var result = cars.Any(c => c.Type != ((Enums.CarType)carType).ToString());

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void CarSearch_ReturnOnlyGivenCarClass()
        {
            var carClassList = (int[])Enum.GetValues(typeof(Enums.CarClass));

            foreach (var carClass in carClassList)
            {
                var carQuery = new CarQueryModel
                {
                    CarClassIds = new List<int> { carClass }
                };
                var cars = _carService.GetList(carQuery);

                var result = cars.Any(c => c.Class != ((Enums.CarClass)carClass).ToString());

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void CarSearch_ReturnOnlyGivenSearchTerm()
        {
            var carQuery = new CarQueryModel
            {
                SearchTerm = "Acura"
            };
            var cars = _carService.GetList(carQuery);

            var result = cars.Any(c => !c.Name.Contains("Acura"));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CarSearch_ReturnValuesSorted()
        {
            var sortTypeList = (int[])Enum.GetValues(typeof(Enums.SortType));

            foreach (var sortType in sortTypeList)
            {
                var carQuery = new CarQueryModel
                {
                    SortById = sortType
                };
                var cars = _carService.GetList(carQuery);
                IEnumerable<CarModel> orderedCars = new List<CarModel>();

                switch (sortType)
                {
                    case (int)Enums.SortType.Name:
                        orderedCars = cars.OrderBy(c => c.Name);
                        break;

                    case (int)Enums.SortType.Price:
                        orderedCars = cars.OrderBy(c => c.Price);
                        break;

                    case (int)Enums.SortType.Odometer:
                        orderedCars = cars.OrderBy(c => c.Odometer);
                        break;

                    case (int)Enums.SortType.ManufactureDate:
                        orderedCars = cars.OrderBy(c => c.ManufactureDate);
                        break;
                }

                var result = orderedCars.Equals(cars);

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void CarSearch_ReturnEmptyListWhenBrandIdDoesNotExist()
        {
            var carQuery = new CarQueryModel
            {
                CarBrandIds = new List<int> { int.MaxValue }
            };

            var cars = _carService.GetList(carQuery);

            Assert.IsFalse(cars.Any());
        }

        [TestMethod]
        public void CarSearch_ReturnEmptyListWhenTypeIdDoesNotExist()
        {
            var carQuery = new CarQueryModel
            {
                CarTypeIds = new List<int> { int.MaxValue }
            };

            var cars = _carService.GetList(carQuery);

            Assert.IsFalse(cars.Any());
        }

        [TestMethod]
        public void CarSearch_ReturnEmptyListWhenClassIdDoesNotExist()
        {
            var carQuery = new CarQueryModel
            {
                CarClassIds = new List<int> { int.MaxValue }
            };

            var cars = _carService.GetList(carQuery);

            Assert.IsFalse(cars.Any());
        }
    }
}
