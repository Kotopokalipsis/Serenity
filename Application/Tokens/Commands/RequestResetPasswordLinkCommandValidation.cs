using FluentValidation;

namespace Application.Tokens.Commands
{
    public class RequestResetPasswordLinkCommandValidation : AbstractValidator<RequestResetPasswordLinkCommand>
    {
        public RequestResetPasswordLinkCommandValidation()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
        }
    }
}