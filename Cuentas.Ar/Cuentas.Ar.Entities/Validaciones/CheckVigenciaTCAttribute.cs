using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuentas.Ar.Entities.Validaciones
{
    public class CheckVigenciaTC : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                {
                    var mes = Convert.ToInt32(value.ToString().Split('/')[0]);
                    var anio = Convert.ToInt32(string.Format("{0}{1}", DateTime.Now.Year.ToString().Substring(0, 2), value.ToString().Split('/')[1]));

                    if (anio < DateTime.Now.Year)
                    {
                        return new ValidationResult("La tarjeta ingresada se encuentra vencida.");
                    }
                    else
                    {
                        if (anio == DateTime.Now.Year && mes < DateTime.Now.Month)
                        {
                            return new ValidationResult("La tarjeta ingresada se encuentra vencida.");
                        }
                        else
                        {
                            return ValidationResult.Success;
                        }
                    }
                }
                else
                {
                    return new ValidationResult("Ingrese un vencimiento de tarjeta.");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(ex.Message);
            }
        }
    }
}
