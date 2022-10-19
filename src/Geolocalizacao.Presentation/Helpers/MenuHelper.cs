using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Presentation.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Geolocalizacao.Presentation.Helpers
{
    public class MenuHelper
    {
        private readonly List<Menu> _menu = new List<Menu>();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string GetMenuAtual()
        {
            var context = _httpContextAccessor.HttpContext;

            var routeData = context.GetRouteData();
            return "/" + routeData.Values["Controller"].ToString().Replace("Controller", "") + "/" + routeData.Values["Action"];
        }

        public List<Menu> GetMenus()
        {
            return _menu;
        }

        public List<ClaimViewModel> GetClaims()
        {
            var permissoes = new List<ClaimViewModel>();
            string[] methods = { "Get", "Post", "Put", "Delete" };

            foreach (var item in _menu)
            {
                var controller = item.Controller.Replace("Controller", "");
                if (item.Crud)
                {
                    permissoes.AddRange(methods.Select(m => new ClaimViewModel(controller, m)));
                }
                else
                {
                    permissoes.Add(new ClaimViewModel(controller, $"Get"));
                }
            }

            return permissoes;
        }

        public MenuHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            _menu.AddRange(
                new List<Menu>
                {                    
                    new Menu()
                    {
                        Controller = nameof(ClienteController),
                        Crud = true
                    },
                    new Menu()
                    {
                        Controller = nameof(PerfilUsuarioController),
                        Crud = true
                    },
                    new Menu()
                    {
                        Controller = nameof(SetoresCensitariosController),
                        Crud = true
                    },
                    new Menu()
                    {
                        Controller = nameof(UsuarioController),
                        Crud = true
                    }
                }
                
           );
        }

    }
}