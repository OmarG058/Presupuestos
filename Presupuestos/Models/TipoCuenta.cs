using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Presupuestos.Validaciones; // Asegúrate de que la ruta sea correcta

namespace Presupuestos.Models
{
    public class TipoCuenta //:IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "La longitudad de {0} debe estar entre {2} y {1}")]
        [Display(Name = "Nombre del Tipo de Cuenta")]
        [PrimeraLetraMayuculaAtributte]
        [Remote(action: "VerificarExisteTipoCuenta", controller: "TiposCuentas")]
        public string Nombre { get; set; } //ctrl + R para renombrar
        public int UsuarioId { get; set; }
        public int Orden { get; set; }

        //Validaciones personalizadas por modelo
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Nombre != null && Nombre.Length > 0)
        //    {
        //        var primeraLetra = Nombre[0].ToString();
        //        if (primeraLetra != primeraLetra.ToUpper()) 
        //        {
        //            yield return new ValidationResult($"La primera letra debe ser mayuscula", new[] {nameof(Nombre)}); 
        //        }
        //    }
        //}
    }
}
