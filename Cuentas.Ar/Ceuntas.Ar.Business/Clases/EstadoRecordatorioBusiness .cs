using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class EstadoRecordatorioBusiness
    {
        private readonly EstadoRecordatorioRepository repositorio;

        public EstadoRecordatorioBusiness()
        {
            this.repositorio = new EstadoRecordatorioRepository();
        }

        public List<EstadoRecordatorio> Listar()
        {
            return repositorio.Listar();
        }
    }
}