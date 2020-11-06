using FluentValidation;
using SocialMediaCore.DTOs;
using System;

namespace SocialMediaInfraestructure.Validators
{
    public class PostValidator : AbstractValidator<PostDTO>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .Length(5, 10)
                .WithMessage("La descripcion no puede ser nula");

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);
        }
    }
}
