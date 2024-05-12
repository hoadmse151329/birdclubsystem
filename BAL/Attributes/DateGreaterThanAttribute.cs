using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        private readonly int? _comparisonRange;
        private readonly string? _comparisonType;

        // Set the name of the property to compare
        public DateGreaterThanAttribute(string comparisonProperty, int comparisonRange, string comparisonType)
        {
            _comparisonProperty = comparisonProperty;
            _comparisonRange = comparisonRange;
            _comparisonType = comparisonType;
        }
        public DateGreaterThanAttribute(string comparisonProperty, string comparisonType)
        {
            _comparisonProperty = comparisonProperty;
            _comparisonType = comparisonType;
        }
        public DateGreaterThanAttribute(string comparisonProperty, int comparisonRange)
        {
            _comparisonProperty = comparisonProperty;
            _comparisonRange = comparisonRange;
        }
        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        // Validate the date comparison
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;

            var comparisonValue = (DateTime)validationContext.ObjectType.GetProperty(_comparisonProperty)
                                                                        .GetValue(validationContext.ObjectInstance);
            if (_comparisonType != null)
            {
                switch (_comparisonType)
                {
                    case "Day":
                        {
                            if (_comparisonRange != null)
                            {
                                if (currentValue.CompareTo(comparisonValue.AddDays(_comparisonRange.Value)) < 0)
                                {
                                    return new ValidationResult(ErrorMessage);
                                }
                            }
                            else
                            {
                                if (currentValue.CompareTo(comparisonValue) <= 0)
                                {
                                    return new ValidationResult(ErrorMessage);
                                }
                            }
                            break;
                        }
                    case "Hour":
                        {
                            if (_comparisonRange != null)
                            {
                                if (currentValue.CompareTo(comparisonValue.AddHours(_comparisonRange.Value)) < 0)
                                {
                                    return new ValidationResult(ErrorMessage);
                                }
                            }
                            else
                            {
                                if (currentValue.CompareTo(comparisonValue) <= 0)
                                {
                                    return new ValidationResult(ErrorMessage);
                                }
                            }
                            break;
                        }
                    case "Month":
                        {
                            if (_comparisonRange != null)
                            {
                                if (currentValue.CompareTo(comparisonValue.AddMonths(_comparisonRange.Value)) < 0)
                                {
                                    return new ValidationResult(ErrorMessage);
                                }
                            }
                            else
                            {
                                if (currentValue.CompareTo(comparisonValue) <= 0)
                                {
                                    return new ValidationResult(ErrorMessage);
                                }
                            }
                            break;
                        }
                    case "Year":
                        {
                            if (_comparisonRange != null)
                            {
                                if (currentValue.CompareTo(comparisonValue.AddYears(_comparisonRange.Value)) < 0)
                                {
                                    return new ValidationResult(ErrorMessage);
                                }
                            }
                            else
                            {
                                if (currentValue.CompareTo(comparisonValue) <= 0)
                                {
                                    return new ValidationResult(ErrorMessage);
                                }
                            }
                            break;
                        }
                    default:
                        {
                            if (currentValue.CompareTo(comparisonValue) <= 0)
                            {
                                return new ValidationResult(ErrorMessage);
                            }
                            break;
                        }
                }
            }
            else
            {
                if (currentValue.CompareTo(comparisonValue) <= 0)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
