using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using ZAP.API.Authorization;
using ZAP.BusinessLogic.DTO.Reservation;
using ZAP.BusinessLogic.Services;
using ZAP.Common.Enums;

namespace ZAP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ReservationService _reservationService;
        private readonly ILogger<CarController> _logger;

        public ReservationController(UserService userService, ReservationService reservationService, ILogger<CarController> logger)
        {
            _userService = userService;
            _reservationService = reservationService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Add")]
        [ZapAuthorize]
        public IActionResult Add([FromBody] ReservationModel model)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = _userService.GetLoggedInUserId(token);

                if (!_reservationService.IsBlacklisted(userId) && _reservationService.CanReserve(model.CarId, model.StartDate, model.EndDate))
                {
                    _reservationService.AddReservation(model, userId, _userService.IsAdmin(token));
                    return new JsonResult(true);
                }
                else
                {
                    return new JsonResult(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to add reservation " + ex);
                return new JsonResult(false);
            }
        }

        [HttpPost]
        [Route("Remove")]
        [ZapAuthorize]
        public IActionResult Remove([FromQuery] int reservationId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = _userService.GetLoggedInUserId(token);
                var isAdmin = _userService.IsAdmin(token);

                if (_reservationService.HasAccess(reservationId, userId) || isAdmin)
                {
                    _reservationService.RemoveReservation(reservationId);
                    return new JsonResult(true);
                }

                return new JsonResult(false);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to add reservation " + ex);
                return new JsonResult(false);
            }
        }

        [HttpGet]
        [Route("UserList")]
        [ZapAuthorize]
        public IActionResult UserList([FromQuery] int userId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var tokenUserId = _userService.GetLoggedInUserId(token);
                var isAdmin = _userService.IsAdmin(token);
                
                // Users can not see other users reservations unless they are admins
                if(tokenUserId != userId && !isAdmin)
                {
                    return Unauthorized();
                }

                return new JsonResult(_reservationService.GetUserReservations(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to add reservation " + ex);
                return new JsonResult(false);
            }
        }

        [HttpGet]
        [Route("CarList")]
        [ZapAuthorize(UserRoleType.Admin)]
        public IActionResult CarList([FromQuery] int carId)
        {
            try
            {
                return new JsonResult(_reservationService.GetCarReservations(carId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to add reservation " + ex);
                return new JsonResult(false);
            }
        }
    }
}
