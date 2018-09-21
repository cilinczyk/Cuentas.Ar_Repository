using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuentas.Ar.Entities.Validaciones
{
    public class CheckCreditCardAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var formaPago = validationContext.ObjectInstance as M_FormaPago;
            var idTipoTarjeta = formaPago.idTipoTarjeta;
            var nroTarjeta = value.ToString();

            try
            {
                if (idTipoTarjeta != ObtenerEntidadTarjeta(nroTarjeta))
                {
                    return new ValidationResult("La tarjeta ingresada no corresponde con la entidad seleccionada.");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(ex.Message);
            }

            #region [Región: Algoritmo de Luhn]
            int sum = 0;
            int digit = 0;
            int addend = 0;
            bool timesTwo = false;

            for (int i = nroTarjeta.Length - 1; i >= 0; i--)
            {
                digit = Int32.Parse(nroTarjeta.Substring(i, 1));
                if (timesTwo)
                {
                    addend = digit * 2;
                    if (addend > 9)
                    {
                        addend -= 9;
                    }
                }
                else
                {
                    addend = digit;
                }
                sum += addend;
                timesTwo = !timesTwo;
            }
            if (sum % 10 != 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("El número de tarjeta ingresado es incorrecto.");
            }
            #endregion
        }

        private int ObtenerEntidadTarjeta(string nroTarjeta)
        {
            int primerosTresDigitos = Convert.ToInt32(nroTarjeta.Substring(0, 3));

            if (primerosTresDigitos >= 510 && primerosTresDigitos <= 559)
            {
                return eTipoTarjeta.Mastercard;
            }
            else
            {
                int primerosCuatroDigitos = Convert.ToInt32(nroTarjeta.Substring(0, 4));

                if (primerosCuatroDigitos >= 4000 && primerosCuatroDigitos <= 4999)
                {
                    return eTipoTarjeta.Visa;
                }
                else if (primerosCuatroDigitos >= 3400 && primerosCuatroDigitos <= 3799)
                {
                    return eTipoTarjeta.AmericanExpress;
                }
                else
                {
                    throw new Exception("El nro. de tarjeta ingresado no pertenece a las entidades permitidas.");
                }
            }
        }
    }
}
