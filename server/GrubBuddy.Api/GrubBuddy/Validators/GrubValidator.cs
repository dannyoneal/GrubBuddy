using System;
using FluentValidation;
using GrubBuddy.Models;

namespace GrubBuddy.Validators
{
    public class GrubValidator : AbstractValidator<Grub>
    {
        public GrubValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("A valid UserId must be provided");
            RuleFor(x => x.CreatorName).NotEmpty().NotEmpty().WithMessage("A Creator name is required");
            RuleFor(x => x.Location).NotEmpty().NotEmpty().WithMessage("A Grub Location is required");
            RuleFor(x => x.TransportationMethodId).SetValidator(new TransportationEnumValidator());
            RuleFor(x => x.GrubTimeUtc).GreaterThanOrEqualTo(DateTime.Now.AddMinutes(-1)).WithMessage("Grub time must be in the future!");
        }
    }
}