﻿using System.Collections.Generic;
using System.Linq;
using Cuentas.Ar.Entities;

namespace Cuentas.Ar.Repository
{
    public class TipoRegistroRepository
    {
        public List<TipoRegistro> Listar()
        {
            using (var context = new CuentasArEntities())
            {
                return context.TipoRegistro.ToList();
            }
        }
    }
}