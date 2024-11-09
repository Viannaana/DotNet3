using CP3.Domain.Interfaces.Dtos;
using FluentValidation;

namespace CP3.Application.Dtos
{
    public class BarcoDto : IBarcoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Tamanho { get; set; }

        public void Validate()
        {
            var validator = new BarcoDtoValidation();
            validator.ValidateAndThrow(this);
        }
    }

    public class BarcoDtoValidation : AbstractValidator<BarcoDto>
    {
        public BarcoDtoValidation()
        {
            RuleFor(b => b.Nome)
                .NotEmpty().WithMessage("O Nome do barco é obrigatório.");

            RuleFor(b => b.Modelo)
                .NotEmpty().WithMessage("O Modelo do barco é obrigatório.");

            RuleFor(b => b.Ano)
                .GreaterThan(0).WithMessage("O Ano deve ser maior que zero.");

            RuleFor(b => b.Tamanho)
                .GreaterThan(0).WithMessage("O Tamanho deve ser maior que zero.");
        }
    }
}