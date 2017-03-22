using System;
using FluentValidation.Validators;
using GrubBuddy.Enums;

namespace GrubBuddy.Validators
{
    public class TransportationEnumValidator : PropertyValidator
    {
        public TransportationEnumValidator()
            : base("Invalid Transportation Value") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            int transportationId;
            int.TryParse(context.PropertyValue.ToString(), out transportationId);
            var enumVal = (Transportation)Enum.Parse(typeof(Transportation), context.PropertyValue.ToString());

            return Enum.IsDefined(typeof(Transportation), enumVal);
        }
    
    }
}