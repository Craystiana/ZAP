using System;
using System.Collections.Generic;
using System.Linq;
using ZAP.BusinessLogic.DTO.Car;
using ZAP.BusinessLogic.DTO.Generic;
using ZAP.DataAccess.Entities;
using ZAP.DataAccess.Interfaces;

namespace ZAP.BusinessLogic.Services
{
    public class CarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CarModel GetCarDetails(int carId)
        {
            var car = _unitOfWork.CarRepository.Get(carId);

            return new CarModel()
            {
                CarId = car.CarId,
                Type =  car.CarType.Name,
                Class = car.CarClass.Name,
                Brand = car.CarBrand.Name,
                LicensePlate = car.LicensePlate,
                Odometer = car.Odometer,
                ManufactureDate = car.ManufactureDate.Date.ToString("dd/MMM/yyyy"),
                Price = car.Price,
                Picture = ConvertToBase64String(car.Picture),
                Name = car.Name
            };
        }

        public CarEditModel GetCar(int carId)
        {
            var car = _unitOfWork.CarRepository.Get(carId);

            return new CarEditModel()
            {
                CarId = car.CarId,
                Type = car.CarType.CarTypeId,
                Class = car.CarClass.CarClassId,
                Brand = car.CarBrand.CarBrandId,
                LicensePlate = car.LicensePlate,
                Odometer = car.Odometer,
                ManufactureDate = car.ManufactureDate.Date,
                Price = car.Price,
                Picture = ConvertToBase64String(car.Picture),
                Name = car.Name
            };
        }

        public CarDataModel GetCarData()
        {
            return new CarDataModel
            {
                CarTypes = _unitOfWork.CarTypeRepository.GetAll().Select(ct => new GenericModel { Id = ct.CarTypeId, Name = ct.Name}),
                CarClasses = _unitOfWork.CarClassRepository.GetAll().Select(ct => new GenericModel { Id = ct.CarClassId, Name = ct.Name }),
                CarBrands = _unitOfWork.CarBrandRepository.GetAll().Select(ct => new GenericModel { Id = ct.CarBrandId, Name = ct.Name })
            };
        }

        public byte[] ConvertToByteArray(string img)
        {
            return string.IsNullOrEmpty(img) ? null : Convert.FromBase64String(img);
        }

        public string ConvertToBase64String(byte[] byteArray)
        {
            return byteArray == null ? null : Convert.ToBase64String(byteArray);
        }

        public void Add(CarEditModel model)
        {
            var car = new Car
            {
                Name = model.Name,
                CarTypeId = model.Type,
                CarClassId = model.Class,
                CarBrandId = model.Brand,
                LicensePlate = model.LicensePlate,
                Odometer = model.Odometer,
                ManufactureDate = model.ManufactureDate.Date,
                Price = model.Price,
                Picture = ConvertToByteArray(model.Picture),
            };

            _unitOfWork.CarRepository.Add(car);
            _unitOfWork.SaveChanges();
        }

        public void Edit(CarEditModel model)
        {
            var car = _unitOfWork.CarRepository.Get((int)model.CarId);

            car.Name = model.Name;
            car.CarTypeId = model.Type;
            car.CarClassId = model.Class;
            car.CarBrandId = model.Brand;
            car.LicensePlate = model.LicensePlate;
            car.Odometer = model.Odometer;
            car.ManufactureDate = model.ManufactureDate;
            car.Price = model.Price;

            if (model.Picture != null)
            {
                car.Picture = ConvertToByteArray(model.Picture);
            }

            _unitOfWork.SaveChanges();
        }

        public void Delete(int carId)
        {
            _unitOfWork.CarRepository.Remove(_unitOfWork.CarRepository.Get(carId));
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<CarModel> GetList(CarQueryModel model)
        {
            return _unitOfWork.CarRepository.GetList(model.SearchTerm, model.CarTypeIds, model.CarClassIds, model.CarBrandIds, model.SortById)
                                            .Select(c => new CarModel 
                                            {   
                                                CarId = c.CarId,
                                                Name = c.Name,
                                                Brand = c.CarBrand.Name,
                                                Class = c.CarClass.Name,
                                                LicensePlate = c.LicensePlate,
                                                Odometer = c.Odometer,
                                                Picture = ConvertToBase64String(c.Picture),
                                                Price =c.Price,
                                                Type = c.CarType.Name,
                                                ManufactureDate =c.ManufactureDate.ToString()
                                            });
        }
    }
}
