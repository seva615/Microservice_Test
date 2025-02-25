using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using Orchestrator.API.Exceptions;
using Google.Apis.Admin.Directory.directory_v1.Data;
using ICSharpCode.Decompiler.IL;

namespace Orchestrator.API.Services
{
    public class JwtAuthorizationFilter : Attribute, IAuthorizationFilter
    {

        List<string> _roles = new List<string>();
        public JwtAuthorizationFilter(params string[] roles)
        {
            _roles = roles.ToList();
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {

            string authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            IUserClient userClient = context.HttpContext.RequestServices.GetRequiredService<IUserClient>();

            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            UserGetModel user = await userClient.GetUserByJwt(authHeader);

            if (user == null)
            {
                //Console.Error.WriteLine($"Ошибка при валидации токена");
                //context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                throw new Exception($"Ошибка при валидации токена");
            }

            if (_roles != null)
            {
                foreach (var role in _roles)
                {
                    if (user.Role != role)
                    {
                        // Console.Error.WriteLine($"Не достаточно прав");
                        //context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                        throw new Exception($"Не достаточно прав");
                        
                    }
                }
            }
        }        
    }
}
