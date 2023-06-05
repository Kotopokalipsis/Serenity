using FluentValidation;

namespace Application.Records.Commands
{
    public class NewRecordCommandValidation : AbstractValidator<NewRecordCommand>
    {
        public NewRecordCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.VisitedAt).NotEmpty();
            RuleFor(x => x.MedicalCardId).NotEmpty();
            RuleFor(x => x.ServiceCategoryId).NotEmpty();
            RuleFor(x => x.ServiceTypeId).NotEmpty();
        }
    }
}