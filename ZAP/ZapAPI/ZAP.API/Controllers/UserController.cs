using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using ZAP.API.Authorization;
using ZAP.BusinessLogic.DTO.User;
using ZAP.BusinessLogic.Services;
using ZAP.Common.Enums;

namespace ZAP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("All")]
        [ZapAuthorize(UserRoleType.Admin)]
        public IActionResult All()
        {
            return new JsonResult(_userService.GetUserList());
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]LoginModel model)
        {
            try
            {
                var idenity = _userService.VerifyIdentity(model.Email, model.Password);

                if (idenity)
                {
                    var user = _userService.GetUser(model.Email);

                    return Ok(new 
                    { 
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.UserRoleId,
                        Token = JwtService.GenerateToken(user)
                    });
                }

                return Forbid();
            }
            catch
            {
                return Forbid();
            }
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody]RegisterModel model)
        {
            try
            {
                var result = _userService.Register(model);

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while registering user " + ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        [ZapAuthorize(UserRoleType.Admin)]
        public IActionResult RegisterAdmin([FromBody] RegisterModel model)
        {
            try
            {
                var result = _userService.RegisterAdmin(model);

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while registering user " + ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("Profile")]
        [ZapAuthorize]
        public IActionResult Profile()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var result = _userService.GetProfile(_userService.GetLoggedInUserId(token));

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while registering user " + ex);
                return StatusCode(500);
            }
        }


        [HttpPost]
        [Route("Profile")]
        [ZapAuthorize]
        public IActionResult Profile([FromBody] ProfileModel model)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                _userService.UpdateProfile(model, _userService.GetLoggedInUserId(token));

                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while editing user profile " + ex);
                return StatusCode(500);
            }
        }
    }
}
