using FluentValidation;
using FluentValidation.Validators;
using Geolocalizacao.Domain.Commands.Clientes;
using Geolocalizacao.Domain.Validations.Assistants;
using System.Text.RegularExpressions;

namespace Geolocalizacao.Domain.Validations.Clientes
{
    public abstract class ClienteValidation<T> : AbstractValidator<T> where T : ClienteCommand
    {
        protected void ValidarId()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage($"O Id do Cliente é obrigatorio.");
        }
        protected void Validar()
        {           
            RuleFor(c => c.Cnpj)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage($"O CNPJ é obrigatorio.")
                    .Length(14)
                    .WithMessage("O CNPJ deve conter 14 caracteres.")
                    .Custom((value, context) =>
                    {
                        if (!CpfCnpjValidations.IsCnpj(value))
                            context.AddFailure("O CNPJ é inválido.");
                    });

            RuleFor(c => c.InscricaoMunicipal)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage($"A Inscrição Municipal é obrigatorio.")
                    .Length(11, 18)
                    .WithMessage("A Inscrição Municipal deve conter entre 11 e 18 caracteres.");

            RuleFor(c => c.RazaoSocial)
                .NotNull()
                .NotEmpty()
                    .WithMessage($"A Razão Social do Cliente é obrigatoria.")
                .Length(2, 300)
                    .WithMessage("A Razão Social do Cliente deve conter entre 2 e 300 caracteres.");

            RuleFor(c => c.Observacao)
                .MaximumLength(300)
                    .WithMessage("A Observação deve conter no máximo 300 caracteres.");
        }

        protected void ValidarEndereco()
        {
            RuleFor(c => c.Logradouro)
                .NotNull()
                .NotEmpty()
                    .WithMessage($"O Logradouro é obrigatorio.")
                .Length(2, 100)
                    .WithMessage("O Logradouro deve conter entre 2 e 100 caracteres.");

            RuleFor(c => c.Bairro)
                .NotNull()
                .NotEmpty()
                    .WithMessage($"O Bairro é obrigatorio.")
                .Length(2, 75)
                    .WithMessage("O Bairro deve conter entre 2 e 75 caracteres.");

            RuleFor(c => c.Cidade)
                .NotNull()
                .NotEmpty()
                    .WithMessage($"A Cidade é obrigatoria.")
                .Length(2, 100)
                    .WithMessage("A Cidade deve conter entre 2 e 100 caracteres.");

            RuleFor(c => c.Uf)
                .NotNull()
                .NotEmpty()
                    .WithMessage($"A UF é obrigatoria.")
                .Length(2)
                    .WithMessage("A UF deve conter 2 caracteres.");

            RuleFor(c => c.Cep)
                .NotNull()
                .NotEmpty()
                    .WithMessage($"O CEP é obrigatoria.")
                .Length(8)
                    .WithMessage("O CEP deve conter 8 caracteres.")
                .Matches(new Regex("[0-9]{5}[0-9]{3}"))
                    .WithMessage("O CEP é inválido.");
        }

        protected void ValidarContato()
        {
            RuleFor(c => c.Email)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage($"O Email é obrigatorio.")
                    .MaximumLength(256)
                    .WithMessage("O Email deve conter no máximo 256 caracteres.")
                    .EmailAddress(EmailValidationMode.Net4xRegex)
                    .WithMessage("O Email é inválido.");

            RuleFor(c => c.Telefone1)
                .NotNull()
                .NotEmpty()
                .WithMessage($"O Telefone é obrigatorio.")
                .Length(10, 11)
                .WithMessage("O Telefone deve conter entre 10 e 11 caracteres.")
                .Matches(new Regex(@"^[1-9]{2}[9]{0,1}[2-9]{1}[0-9]{3}[0-9]{4}$"))
                .WithMessage(c => $"O Telefone '{c.Telefone1}' é inválido.");

            When(c => !string.IsNullOrEmpty(c.Telefone2), () =>
            {
                RuleFor(c => c.Telefone2)
                .Length(10, 11)
                .WithMessage("O Telefone deve conter entre 10 e 11 caracteres.")
                .Matches(new Regex(@"^[1-9]{2}[9]{0,1}[2-9]{1}[0-9]{3}[0-9]{4}$"))
                .WithMessage(c => $"O Telefone '{c.Telefone2}' é inválido.");
            });
            
        }
    }
}
