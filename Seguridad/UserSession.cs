
using App.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Seguridad
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserSession()
        {
            var UserName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            return UserName;
        }
    }
}
