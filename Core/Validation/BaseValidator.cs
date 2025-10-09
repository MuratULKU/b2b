using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validation
{
    public abstract class BaseValidator<T> : IValidator<T>
    {
        protected List<string> _errors = new();

        public abstract void ValidateRules(T entity);

        public ValidationResult Validate(T entity)
        {
            _errors.Clear();
            ValidateRules(entity);
            return new ValidationResult { Errors = _errors };
        }

        protected void NotEmpty(string value, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
                _errors.Add(errorMessage);
        }

        protected void MaxLength(string value, int maxLength, string errorMessage)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
                _errors.Add(errorMessage);
        }

        protected void Range(decimal value, decimal min, decimal max, string errorMessage)
        {
            if (value < min || value > max)
                _errors.Add(errorMessage);
        }
        protected void Range(double value, double min, double max, string errorMessage)
        {
            if (value < min || value > max)
                _errors.Add(errorMessage);
        }
    }
}
