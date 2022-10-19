using Geolocalizacao.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Geolocalizacao.Infra.CrossCutting.Identity.Models
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => GetName();

        public Guid Id => _accessor.HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier) != null ? Guid.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value) : Guid.Empty;

        public bool IsAdmin
        {
            get
            {
                return _accessor.HttpContext != null && (
                    _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Administrador");
            }

        }

        private string GetName()
        {
            return _accessor.HttpContext?.User.Identity.Name ??
                   _accessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        public bool IsAuthenticated()
        {
            return _context != null && _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext?.User.Claims;
        }

        private HttpContext _context
        {
            get
            {
                return _accessor.HttpContext;
            }
        }
    }
}
