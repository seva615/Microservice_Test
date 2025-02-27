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
using System.Net;

namespace Orchestrator.API.Services
{
    public class JwtAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {

        List<string> _roles = new List<string>();
        public JwtAuthorizationFilter(params string[] roles)
        {
            _roles = roles.ToList();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            
            string authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            IUserClient userClient = context.HttpContext.RequestServices.GetRequiredService<IUserClient>();
            
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            UserGetModel user =  await userClient.GetUserByJwt(authHeader);
            
            if (user == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return;
            }

            if (_roles != null && _roles.Any())
            {
                if (!_roles.Contains(user.Role))
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                    return;
                }
            }
            
        }

       
    }
}
