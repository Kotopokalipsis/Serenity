using FluentValidation;

namespace Application.MedicalCards.Commands
{
    public class NewMedicalCardCommandValidation : AbstractValidator<NewMedicalCardCommand>
    {
        public NewMedicalCardCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}