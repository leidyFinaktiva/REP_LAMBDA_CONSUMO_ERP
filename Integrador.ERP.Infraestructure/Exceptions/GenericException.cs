using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Application.Exceptions
{
    public class GenericException
    {
        public FluentValidation.Results.ValidationResult CrearExcepcion(string code, string error)
        {
            FluentValidation.Results.ValidationResult validationResult = new FluentValidation.Results.ValidationResult();
            validationResult.Errors = new List<ValidationFailure>
            {
                new ValidationFailure { ErrorCode = code, ErrorMessage = error }
            };

            return validationResult;
        }
    }
}
