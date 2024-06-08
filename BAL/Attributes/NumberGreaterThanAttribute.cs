using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NumberGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        private readonly int _comparisonRange = 500;
        private readonly string _comparisonCurrency = "ELO";

        public NumberGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        public NumberGreaterThanAttribute(string comparisonProperty, int comparisonRange)
        {
            _comparisonProperty = comparisonProperty;
            _comparisonRange = comparisonRange;
        }
        public NumberGreaterThanAttribute(string comparisonProperty, int comparisonRange, string comparisonCurrency)
        {
            _comparisonProperty = comparisonProperty;
            _comparisonRange = comparisonRange;
            _comparisonCurrency = comparisonCurrency;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (int?)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (int?)property.GetValue(validationContext.ObjectInstance);

            if (currentValue.HasValue && comparisonValue.HasValue)
            {
                if (currentValue.Value < comparisonValue.Value + _comparisonRange)
                    return new ValidationResult($"The {_comparisonProperty} must be at least {_comparisonRange} {_comparisonCurrency} less than {validationContext.DisplayName}");
            }

            return ValidationResult.Success;
        }
    }
}
