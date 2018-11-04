using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class LocalidadBusiness
    {
        private readonly LocalidadRepository repositorio;

        public LocalidadBusiness()
        {
            this.repositorio = new LocalidadRepository();
        }

        public List<Localidad> Listar(int idProvincia)
        {
            return repositorio.Listar(idProvincia);
        }
    }
}