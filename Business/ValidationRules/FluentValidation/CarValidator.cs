using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        // Validator kuralları constructor içine yazılır.
        public CarValidator()
        {
            RuleFor(c => c.DailyPrice).NotEmpty();
            RuleFor(c => c.DailyPrice).GreaterThan(0);
            RuleFor(c => c.Description).MinimumLength(10);
            RuleFor(c => c.Description).Must(StartWithM).WithMessage("Arabalar M harfi ile başlamalı.");
            RuleFor(c => c.ModelYear).NotEmpty();
            RuleFor(c => c.ModelYear).GreaterThanOrEqualTo(2020).When(c => c.BrandId == 2);
        }

        private bool StartWithM(string arg)
        {
            return arg.StartsWith("M");
        }
    }
}
