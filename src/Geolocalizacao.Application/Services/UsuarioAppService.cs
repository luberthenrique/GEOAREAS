using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Core.Interfaces;
using Geolocalizacao.Domain.Core.Notifications;
using Geolocalizacao.Domain.Entities.Usuarios;
using Geolocalizacao.Domain.Entities.Usuarios.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocalizacao.Application.Services
{
    public class UsuarioAppService : ApplicationBaseService, IUsuarioAppService
    {
        private readonly IUsuarioRepository _repository;
        private readonly UserManager<Usuario> _userManager;
        private readonly IUser _user;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSenderService;

        public UsuarioAppService(
            IUsuarioRepository repository,
            UserManager<Usuario> userManager,
            IUser user,
            IEmailSender emailSenderService,
            IHttpContextAccessor httpContextAccessor,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _repository = repository;
            _userManager = userManager;
            _user = user;

            _emailSenderService = emailSenderService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task UpdateAsync(UsuarioViewModel usuarioViewModel)
        {
            if (await _repository.AnyAsync(c => c.Email == usuarioViewModel.Email && c.Id != usuarioViewModel.Id))
            {
                _notifications.AddNotification("", "O email já foi cadastrado.");
                return;
            }

            if (await _repository.AnyAsync(c => c.Nome == usuarioViewModel.Nome && c.Id != usuarioViewModel.Id))
            {
                _notifications.AddNotification("", "O nome já foi cadastrado.");
                return;
            }

            if (await _repository.AnyAsyncNoQuery(c => c.NormalizedEmail == usuarioViewModel.Email.ToUpper() && c.Id != usuarioViewModel.Id))
            {
                _notifications.AddNotification("", "O e-mail está vinculado a uma conta desativada, entre em contato com o administrador do sistema.");
                return;
            }

            var obj = await _userManager.FindByIdAsync(usuarioViewModel.Id.ToString());
            obj.Atualizar(
                usuarioViewModel.IdPerfil,
                usuarioViewModel.IdUsuario,
                usuarioViewModel.Nome,
                usuarioViewModel.Email,
                usuarioViewModel.Habilitado);

            var retorno = await _userManager.UpdateAsync(obj);

            if (!retorno.Succeeded)
            {
                _notifications.HandleAll(retorno.Errors.Select(c => new DomainNotification("", c.Description)));
                return;
            }
        }

        public async Task UpdateDataAsync(UsuarioDataViewModel usuario)
        {
            var obj = await _repository.GetAll().FirstOrDefaultAsync(c => c.Id == usuario.Id);

            obj.UpdateData(usuario.Nome);

            _repository.Update(obj);

            await _repository.SaveChangesAsync();
        }

        public async Task RegisterAsync(UsuarioViewModel usuarioViewModel)
        {
            if (await _repository.AnyAsync(c => c.Email == usuarioViewModel.Email))
            {
                _notifications.AddNotification("", "O email já foi cadastrado.");
                return;
            }

            if (await _repository.AnyAsync(c => c.Nome == usuarioViewModel.Nome))
            {
                _notifications.AddNotification("", "O nome já foi cadastrado.");
                return;
            }

            if (await _repository.AnyAsyncNoQuery(c => c.NormalizedEmail == usuarioViewModel.Email.ToUpper()))
            {
                _notifications.AddNotification("", "O e-mail está vinculado a uma conta desativada, entre em contato com o administrador do sistema.");
                return;
            }

            var obj = new Usuario(
                usuarioViewModel.Id.GetValueOrDefault(),              
                usuarioViewModel.IdPerfil,
                usuarioViewModel.IdUsuario,
                usuarioViewModel.Nome,
                usuarioViewModel.Email,
                usuarioViewModel.Habilitado);

            var retorno = await _userManager.CreateAsync(obj);
            if (!retorno.Succeeded)
            {
                //TODO - notificar mensagem de erro
                _notifications.HandleAll(retorno.Errors.Select(c => new DomainNotification("", c.Description)));
            }

            string confirmationToken = _userManager.
                GenerateEmailConfirmationTokenAsync(obj).Result;

            var request = _httpContextAccessor.HttpContext.Request;
            var host = $"{request.Scheme}://{request.Host.Value.ToString()}{request.PathBase.Value.ToString()}";

            string confirmationLink = $"{host}/auth/confirmemail?email={obj.Email}&token={confirmationToken}";

            //Carregar layout de envio de email
            var directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "emailLayout", "confirmacaoEmail.html");
            string layout = System.IO.File.ReadAllText(@directory);
            layout = layout.Replace("{nome}", obj.Nome);
            layout = layout.Replace("{linkConfirmacao}", confirmationLink);

            // Não aguardar o envio do e-mail
            _ = _emailSenderService.SendEmailAsync(obj.Email, "Confirme seu email", layout);
        }

        public async Task RegisterConfirmedUserAsync(UsuarioViewModel usuarioViewModel, string senha, string role)
        {
            if (await _repository.AnyAsync(c => c.Email == usuarioViewModel.Email))
            {
                _notifications.AddNotification("", "O email já foi cadastrado.");
                return;
            }

            if (await _repository.AnyAsync(c => c.Nome == usuarioViewModel.Nome))
            {
                _notifications.AddNotification("", "O nome já foi cadastrado.");
                return;
            }

            if (await _repository.AnyAsyncNoQuery(c => c.NormalizedEmail == usuarioViewModel.Email.ToUpper()))
            {
                _notifications.AddNotification("", "O e-mail está vinculado a uma conta desativada, entre em contato com o administrador do sistema.");
                return;
            }

            var obj = new Usuario(
                usuarioViewModel.Id.GetValueOrDefault(),
                usuarioViewModel.IdPerfil,
                usuarioViewModel.IdUsuario,
                usuarioViewModel.Nome,
                usuarioViewModel.Email,
                usuarioViewModel.Habilitado);

            obj.NormalizedEmail = obj.Email.ToUpper();
            obj.NormalizedUserName = obj.Nome.ToUpper();

            var retorno = await _userManager.CreateAsync(obj, senha);
            if (!retorno.Succeeded)
            {
                //TODO - notificar mensagem de erro
                _notifications.HandleAll(retorno.Errors.Select(c => new DomainNotification("", c.Description)));

                return;
            }

            await _userManager.AddToRoleAsync(obj, role);

            obj.EmailConfirmed = true;
            _repository.Update(obj);

            await _repository.SaveChangesAsync();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public async Task RemoveAsync(Guid id)
        {
            await _repository.RemoveAsync(id);

            await _repository.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await _repository.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> AnyAsync(string email)
        {
            return await _repository.AnyAsync(c => c.Email == email);
        }

        public async Task<IEnumerable<UsuarioViewModel>> GetAllAsync()
        {
            var query = _repository.GetAll();

            if (!_user.IsAdmin)
            {
                var idsSubordinados = await ObeterSubordinados();
                query = query.Where(c => idsSubordinados.Contains(c.Id));
            }

            return await query.Select(c => new UsuarioViewModel
            {
                Id = c.Id,
                IdPerfil = c.IdPerfil,
                Email = c.Email,
                Habilitado = c.Habilitado,
                IdUsuario = c.IdUsuario,
                Nome = c.Nome,
                PerfilUsuario = new PerfilUsuarioViewModel
                {
                    Nome = c.PerfilUsuario.Name
                }
            }).ToListAsync();
        }

        public async Task<UsuarioViewModel> GetByIdAsync(Guid id)
        {
            return await _repository.GetAll().Select(c => new UsuarioViewModel
            {
                Id = c.Id,
                IdPerfil = c.IdPerfil,
                Email = c.Email,
                Habilitado = c.Habilitado,
                IdUsuario = c.IdUsuario,
                Nome = c.Nome,
                SenhaCadastrada = !string.IsNullOrEmpty(c.SecurityStamp)
            }).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<UsuarioViewModel> GetByEmailAsync(string email)
        {
            return await _repository.GetAll().Select(c => new UsuarioViewModel
            {
                Id = c.Id,
                IdPerfil = c.IdPerfil,
                Email = c.Email,
                Habilitado = c.Habilitado,
                Nome = c.Nome,
                SenhaCadastrada = c.PasswordHash != null
            }).FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Guid?> GetIdProfileAsync(Guid idUser)
        {
            return await _repository.GetAll().Where(c => c.Id == idUser)?.Select(c => c.IdPerfil).FirstOrDefaultAsync();
        }
    
        private async Task<IEnumerable<Guid>> ObeterSubordinados()
        {
            return await _repository.ObterSubordinados(_user.Id);
        }
    }
}
