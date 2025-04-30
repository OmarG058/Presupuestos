using System.ComponentModel.DataAnnotations;

namespace Presupuestos.Validaciones
{
    public class PrimeraLetraMayuculaAtributte:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primearaLetra = value.ToString()[0].ToString();
            if (primearaLetra != primearaLetra.ToUpper()) 
            {
                return new ValidationResult($"La primera letra de {validationContext.DisplayName} debe ser mayúscula"); 

            }

            return ValidationResult.Success;    
        }
    }
}
