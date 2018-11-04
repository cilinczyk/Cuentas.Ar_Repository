using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class ProvinciaBusiness
    {
        private readonly ProvinciaRepository repositorio;

        public ProvinciaBusiness()
        {
            this.repositorio = new ProvinciaRepository();
        }

        public List<Provincia> Listar()
        {
            return repositorio.Listar();
        }
    }
}