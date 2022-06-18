using Domain.Entities;
using FluentValidation;

namespace Vivo.Validators.V1
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {

        public ClienteValidator()
        {
            RuleFor(x => x.NomeCompleto).NotEmpty().WithMessage("O nome deve ser informado.");
            RuleFor(x => x.DataNascimento).NotEmpty().WithMessage("Data de nascimento deve ser informada.");
            RuleFor(x => x.DocRgNumero).NotEmpty().Must(x => x.Length == 7).Matches(@"^\d{7}$").WithMessage("O RG deve conter 7 dígitos numéricos.");
            RuleFor(x => x.DocRgOrgaoEmissor).NotEmpty().WithMessage("O órgão emissor deve ser informado.");
            RuleFor(x => x.DocRgUF).NotEmpty().Must(x => x.Length == 2).WithMessage("A UF do RG deve ser informada e conter 2 dígitos.");
            RuleFor(x => x.DocCPF).NotEmpty().Must(x => x.Length == 11).Matches(@"^\d{11}$").WithMessage("O CPF deve conter 11 dígitos numéricos.");
            RuleFor(x => x.Endereco).NotEmpty().WithMessage("O endereço deve ser informado.");
            RuleFor(x => x.Ativo).Must(x => x == 0 || x == 1).WithMessage("O status do usuário deve ser informado usando 0 ou 1.");
            RuleFor(x => x.DataCadastro).NotEmpty().WithMessage("A data de cadastro deve ser informada.");

        }
    }
}
