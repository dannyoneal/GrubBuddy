using System;
using FluentValidation;
using GrubBuddy.Models;
using GrubBuddy.DataAccess.Interfaces;

namespace GrubBuddy.Validators
{
    public class GrubValidator : AbstractValidator<Grub>
    {
        public GrubValidator(IUserRepository userRepo)
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .Must((x, y, z) => userRepo.DoesUserExist(x.UserId))
                .WithMessage("A valid UserId must be provided");
            RuleFor(x => x.CreatorName).NotEmpty().NotEmpty().WithMessage("A Creator name is required");
            RuleFor(x => x.Location).NotEmpty().NotEmpty().WithMessage("A Grub Location is required");
            RuleFor(x => x.TransportationMethodId).SetValidator(new TransportationEnumValidator()).WithMessage("Invalid transportation method!");
            RuleFor(x => x.GrubTimeUtc).GreaterThanOrEqualTo(DateTime.Now.AddMinutes(-1)).WithMessage("Grub time must be in the future!");
        }
    }
}