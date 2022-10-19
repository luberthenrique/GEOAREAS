using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Core.Notifications;
using Geolocalizacao.Domain.Entities.Usuarios;
using Geolocalizacao.Domain.Entities.Usuarios.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Geolocalizacao.Application.Services
{
    public class PerfilUsuarioAppService : ApplicationBaseService, IPerfilUsuarioAppService
    {
        private readonly IPerfilUsuarioRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly RoleManager<PerfilUsuario> _roleManager;
        public PerfilUsuarioAppService(
            IPerfilUsuarioRepository repository,
            IUsuarioRepository usuarioRepository,
            RoleManager<PerfilUsuario> roleManager,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            _roleManager = roleManager;
        }

        public async Task UpdateAsync(PerfilUsuarioViewModel perfilUsuarioViewModel)
        {
            if (await _repository.GetAll().AnyAsync(c => c.Name == perfilUsuarioViewModel.Nome && c.Id != perfilUsuarioViewModel.Id))
            {
                _notifications.AddNotification("", $"O nome já foi cadastrado.");
                return;
            }
            var perfil = await _roleManager.FindByIdAsync(perfilUsuarioViewModel.Id.ToString());

            perfil.Atualizar(
                perfilUsuarioViewModel.IsAdmin,
                perfilUsuarioViewModel.Nome);

            var retorno = await _roleManager.UpdateAsync(perfil);

            if (!retorno.Succeeded)
            {
                _notifications.HandleAll(retorno.Errors.Select(c => new DomainNotification("", c.Description)));
                return;
            }

            //Claims salvas no bd para o perfil
            var claims = await _roleManager.GetClaimsAsync(perfil);

            //Remover as claims que estão no banco e não estão nas permissões
            foreach (var claim in claims.Where(c => !perfilUsuarioViewModel.Claims.Select(p => p.Type + p.Value).ToList().Contains(c.Type + c.Value)))
            {
                await _roleManager.RemoveClaimAsync(perfil, claim);
            }

            //Adicionar as claims que ainda não foram salvas no bd
            foreach (var item in perfilUsuarioViewModel.Claims.Where(c => !claims.Select(d => d.Type + d.Value).ToList().Contains(c.Type + c.Value)))
            {
                await _roleManager.AddClaimAsync(perfil, new Claim(item.Type, item.Value));
            }

        }

        public async Task RegisterAsync(PerfilUsuarioViewModel perfilUsuarioViewModel)
        {
            if (await _repository.GetAll().AnyAsync(c => c.Name == perfilUsuarioViewModel.Nome))
            {
                _notifications.AddNotification("", $"O nome já foi cadastrado.");
                return;
            }

            var obj = new PerfilUsuario(
                perfilUsuarioViewModel.Id.GetValueOrDefault(),
                perfilUsuarioViewModel.IsAdmin,
                perfilUsuarioViewModel.Nome,
                perfilUsuarioViewModel.ConcurrencyStamp);

            if (await _roleManager.RoleExistsAsync(obj.Name))
            {
                //TODO: RETORNAR ERRO DE ROLE EXISTENTE
                return;
            }

            var result = await _roleManager.CreateAsync(obj);

            if (!result.Succeeded)
            {
                //TODO: RETORNAR O ERRO OCORRIDO AO CRIAR ROLE
                return;
            }

            foreach (var permissao in perfilUsuarioViewModel.Claims)
            {
                result = await _roleManager.AddClaimAsync(obj, new Claim(permissao.Type, permissao.Value));

                if (!result.Succeeded)
                {
                    //TODO: RETORNAR O ERRO OCORRIDO AO CRIAR claim
                    return;
                }
            }

            return;
        }

        public async Task RegisterNewClaims(PerfilUsuarioViewModel perfilUsuarioViewModel)
        {
            var role = await _roleManager.FindByNameAsync(perfilUsuarioViewModel.Nome);

            var claimsBd = _roleManager.GetClaimsAsync(role).GetAwaiter().GetResult();
            // adicionar permissoes
            foreach (var permissao in perfilUsuarioViewModel.Claims)
            {
                if (!claimsBd.Any(c => c.Type == permissao.Type && c.Value == permissao.Value))
                {
                   var result = await _roleManager.AddClaimAsync(role, new Claim(permissao.Type, permissao.Value));                    
                }
            }
        }
        public void Dispose()
        {
            _repository.Dispose();
        }

        public async Task RemoveAsync(Guid id)
        {
            if (await _usuarioRepository.GetAll().AnyAsync(c => c.IdPerfil == id))
            {
                _notifications.AddNotification("", $"Existe(m) usuário(s) relacionado(s) ao pefil selecionado.");
                return;
            }

            await _repository.DeleteAsync(id);

            await _repository.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await _repository.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> AnyAsync(string name)
        {
            return await _repository.AnyAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<PerfilUsuarioViewModel>> GetAllAsync()
        {
            return await _repository.GetAll().Select(c => new PerfilUsuarioViewModel
            {
                Id = c.Id,
                ConcurrencyStamp = c.ConcurrencyStamp,
                IsAdmin = c.IsAdmin,
                Nome = c.Name,
                NomeNormalizado = c.NormalizedName
            }).ToListAsync();
        }

        public async Task<PerfilUsuarioViewModel> GetByIdAsync(Guid id)
        {
            var perfil = await _repository.GetByIdAsync(id);
            return new PerfilUsuarioViewModel
            {
                Id = perfil.Id,
                IsAdmin = perfil.IsAdmin,
                ConcurrencyStamp = perfil.ConcurrencyStamp,
                Nome = perfil.Name,
                NomeNormalizado = perfil.NormalizedName,
                Claims = (await _roleManager.GetClaimsAsync(perfil))?.Select(c => new ClaimViewModel(c.Type, c.Value)).ToList()
            };
        }

        public async Task<PerfilUsuarioViewModel> GetByNameAsync(string name)
        {
            var perfil = await _repository.GetAll().FirstOrDefaultAsync(c => c.Name == name);

            return new PerfilUsuarioViewModel
            {
                Id = perfil.Id,
                IsAdmin = perfil.IsAdmin,
                ConcurrencyStamp = perfil.ConcurrencyStamp,
                Nome = perfil.Name,
                NomeNormalizado = perfil.NormalizedName,
                Claims = (await _roleManager.GetClaimsAsync(perfil))?.Select(c => new ClaimViewModel(c.Type, c.Value)).ToList()
            };
        }

    }
}
