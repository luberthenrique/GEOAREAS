using FluentValidation;
using Geolocalizacao.Domain.Commands.SetoresCensitarios;
using System.IO;
using System.Linq;

namespace Geolocalizacao.Domain.Validations.SetoresCensitarios
{
    public abstract class SetoresCensitariosValidation<T> : AbstractValidator<T> where T : SetoresCensitarioCommand
    {
        protected void ValidarId()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage($"O Id do Cliente é obrigatorio.");
        }

        protected void ValidarNome()
        {
            RuleFor(c => c.Nome)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage($"O Nome é obrigatorio.")
                    .Length(2, 100)
                    .WithMessage("O Nome deve conter entre 2 e 100 caracteres.");            
        }

        protected void ValidarArquivos()
        {
            RuleFor(c => c.Files)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage($"O carregamento de arquivos de Setores Censitários é obrigatorio.");


            When(c => c.Files.Any(), () =>
            {
                RuleFor(c => c.Files)
                .Must(c => c.Any(f => Path.GetExtension(f.FileName) == ".dbf"))
                    .WithMessage("O carregamento de um arquivo '.dbf' é obrigatório.");

                RuleFor(c => c.Files)
                .Must(c => c.Count(f => Path.GetExtension(f.FileName) == ".dbf") <= 1)
                    .WithMessage("O carregamento de multiplos arquivo '.dbf' não é permitido.");

                RuleFor(c => c.Files)
                .Must(c => c.Any(f => Path.GetExtension(f.FileName) == ".shp"))
                    .WithMessage("O carregamento de um arquivo '.shp' é obrigatório.");

                RuleFor(c => c.Files)
                .Must(c => c.Count(f => Path.GetExtension(f.FileName) == ".shp") <= 1)
                    .WithMessage("O carregamento de multiplos arquivo '.shp' não é permitido.");

                RuleForEach(c => c.Files).ChildRules(file =>
                {
                    file.RuleFor(c => c.Length)
                    .Must(c => c / (1024 * 1024) <= 200)
                    .WithMessage(c => $"O arquivo '{c.FileName}' possui tamanho maior do que o limite de 200MB permitido.");
                });                
            });
        }
    }
}
