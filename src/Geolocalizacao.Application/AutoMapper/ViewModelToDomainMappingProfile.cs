using AutoMapper;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Commands.Clientes;
using Geolocalizacao.Domain.Commands.SetoresCensitarios;

namespace Geolocalizacao.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region Setores Censitários
            CreateMap<ArquivoSetoresCensitariosUploadViewModel, RegistrarSetoresCensitariosCommand>()
                .ConstructUsing(c => new RegistrarSetoresCensitariosCommand(
                    c.Nome,
                    c.Files
                        )
                );
            #endregion

            #region Cliente
            CreateMap<ClienteViewModel, RegistrarClienteCommand>()
                .ConstructUsing(c => new RegistrarClienteCommand(
                    c.Cnpj,
                    c.InscricaoMunicipal,
                    c.RazaoSocial,
                    c.Observacao,
                    c.Logradouro,
                    c.Numero,
                    c.Complemento,
                    c.Bairro,
                    c.Cidade,
                    c.Uf,
                    c.Cep,
                    c.Telefone1,
                    c.Telefone2,
                    c.Email
                        )
                );

            CreateMap<ClienteViewModel, AlterarClienteCommand>()
                .ConstructUsing(c => new AlterarClienteCommand(
                    c.Id.Value,
                    c.Cnpj,
                    c.InscricaoMunicipal,
                    c.RazaoSocial,
                    c.Observacao,
                    c.Logradouro,
                    c.Numero,
                    c.Complemento,
                    c.Bairro,
                    c.Cidade,
                    c.Uf,
                    c.Cep,
                    c.Telefone1,
                    c.Telefone2,
                    c.Email
                    )
                );

            CreateMap<ClienteViewModel, RemoverClienteCommand>()
                .ConstructUsing(c => new RemoverClienteCommand(
                    c.Id.GetValueOrDefault(System.Guid.Empty)
                    ));

            CreateMap<ApiClienteViewModel, RegistrarApiClienteCommand>()
                .ConstructUsing(c => new RegistrarApiClienteCommand(
                    c.IdCliente.GetValueOrDefault(System.Guid.Empty),
                    c.Nome
                    ));

            CreateMap<ApiClienteViewModel, RemoverApiClienteCommand>()
                .ConstructUsing(c => new RemoverApiClienteCommand(
                    c.Id.GetValueOrDefault(System.Guid.Empty)
                    ));

            #endregion
        }

    }
}
