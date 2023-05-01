using FluentValidation;

namespace Application.Users.Queries;

public class GetUserQueryValidation : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidation()
    {
        CascadeMode = CascadeMode.Continue;

        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}
