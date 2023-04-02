using System.Data;
using core.Models;
using FluentValidation;

namespace web_api_min_ex.Validators;

public class StudentValidator: AbstractValidator<NewStudent>
{
    public StudentValidator()
    {
        RuleFor(s => s.LastName).MaximumLength(15).MinimumLength(3);
        RuleFor(s => s.FirstName).MinimumLength(3).MaximumLength(25);
        RuleFor(s => s.Birth).GreaterThan(DateOnly.Parse("2001-12-31"));
        RuleFor(s => s.Phone).Length(9,12).Matches("^[0-9]*$");
    }
}