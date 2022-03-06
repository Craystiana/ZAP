using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using ZAP.API.Authorization;
using ZAP.BusinessLogic.DTO.Car;
using ZAP.BusinessLogic.Services;
using ZAP.Common.Enums;

namespace ZAP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;
        private readonly ILogger<CarController> _logger;

        public CarController(CarService carService, ILogger<CarController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        [HttpGet]
        [Route("Detail")]
        [ZapAuthorize]
        public IActionResult Detail([FromQuery] int carId)
        {
            try
            {
                return new JsonResult(_carService.GetCarDetails(carId));
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to fetch car details for car with id " + carId + ".\nError:\n" + e);
                return new JsonResult(new CarModel());
            }
        }

        [HttpGet]
        [Route("Data")]
        [ZapAuthorize]
        public IActionResult Data()
        {
            try
            {
                return new JsonResult(_carService.GetCarData());
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to fetch car data.\nError:\n" + e);
                return new JsonResult(new CarDataModel());
            }
        }

        [HttpGet]
        [Route("Edit")]
        [ZapAuthorize(UserRoleType.Admin)]
        public IActionResult Edit([FromQuery]int carId)
        {
            try
            {
                return new JsonResult(_carService.GetCar(carId));
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to fetch car details for car with id " + carId + ".\nError:\n" + e);
                return new JsonResult(new CarEditModel());
            }
        }

        [HttpPost]
        [Route("Edit")]
        [ZapAuthorize(UserRoleType.Admin)]
        public IActionResult Edit([FromBody] CarEditModel model)
        {
            try
            {
                if(model.CarId != null)
                {
                    _carService.Edit(model);
                }
                else
                {
                    _carService.Add(model);
                }

                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while adding the car " + ex);
                return new JsonResult(false);
            }
        }

        [HttpPost]
        [Route("List")]
        [ZapAuthorize]
        public IActionResult List([FromBody] CarQueryModel model)
        {
            try
            {
                return new JsonResult(_carService.GetList(model));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetching the car list " + ex);
                return new JsonResult(false);
            }
        }

        [HttpPost]
        [Route("Delete")]
        [ZapAuthorize(UserRoleType.Admin)]
        public IActionResult Delete([FromQuery]int carId)
        {
            try
            {
                _carService.Delete(carId);
                return new JsonResult(true);
            }
            catch(Exception ex)
            {
                _logger.LogError("Unable to delete car with id " + carId + ".\nError:\n" + ex);
                return new JsonResult(false);
            }
        }
    }
}
