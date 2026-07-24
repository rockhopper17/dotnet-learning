using FluentValidation;
using LSC.SmartCertify.Application.DTOs;
using LSC.SmartCertify.Application.Interfaces.Courses;

namespace LSC.SmartCertify.Application.DTOValidations;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseDto>
{
    public UpdateCourseValidator(ICourseRepository courseRepository)
    {
        RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(100)
            .MustAsync(async (title,cancellation) =>
                title == null || !await courseRepository.IsTitleDuplicateAsync(title))
            .WithMessage("the course title must be unique, the title passed already exists");
        RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(500);
    }
}