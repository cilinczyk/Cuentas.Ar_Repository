using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class MonedaBusiness
    {
        private readonly MonedaRepository repositorio;

        public MonedaBusiness()
        {
            this.repositorio = new MonedaRepository();
        }

        public List<Moneda> Listar()
        {
            return repositorio.Listar();
        }
    }
}