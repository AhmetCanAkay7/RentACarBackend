using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarDetailsDtoValidator : AbstractValidator<CarDetailsDto>
    {
        public CarDetailsDtoValidator()
        {
            RuleFor(dto => dto.DailyPrice).NotEmpty();
            RuleFor(dto => dto.CarName).NotEmpty();
            RuleFor(dto => dto.BrandName).NotEmpty();
            RuleFor(dto => dto.ColorName).NotEmpty();
            RuleFor(dto => dto.DailyPrice).GreaterThan(0);
        }
    }
}
