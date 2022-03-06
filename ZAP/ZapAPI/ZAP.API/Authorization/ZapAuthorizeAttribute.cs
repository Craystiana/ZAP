using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using ZAP.Common;
using ZAP.Common.Enums;

namespace ZAP.API.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ZapAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly int[] _userRoles;

        public ZapAuthorizeAttribute()
        {
            _userRoles = new int[]
            {
               (int) UserRoleType.Customer,
               (int) UserRoleType.Admin
            };
        }

        public ZapAuthorizeAttribute(UserRoleType userRole)
        {
            _userRoles = new int[]
            {
                (int) userRole
            };
        }

        public ZapAuthorizeAttribute(UserRoleType[] userRoles)
        {
            _userRoles = (int[]) userRoles.Select(ur => (int)ur);
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();

                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Settings.TokenSecretBytes),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userRoleId = int.Parse(jwtToken.Claims.First(x => x.Type == ((int)TokenClaim.UserRole).ToString()).Value);

                    if (!_userRoles.Contains(userRoleId))
                    {
                        context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    }
                }
                else
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
            catch
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            
        }
    }
}
