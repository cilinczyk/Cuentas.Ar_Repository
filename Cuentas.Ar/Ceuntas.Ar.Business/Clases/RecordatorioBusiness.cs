using System;
using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class RecordatorioBusiness
    {
        private readonly RecordatorioRepository repositorio;

        public RecordatorioBusiness()
        {
            this.repositorio = new RecordatorioRepository();
        }

        public List<Recordatorio> Listar()
        {
            return repositorio.Listar();
        }

        public List<Recordatorio> Listar(int idUsuario, DateTime desde, DateTime hasta)
        {
            return repositorio.Listar(idUsuario, desde, hasta);
        }

        public Recordatorio Obtener(int idRecordatorio)
        {
            return repositorio.Obtener(idRecordatorio);
        }

        public Recordatorio ObtenerCompleto(int idRecordatorio)
        {
            return repositorio.ObtenerCompleto(idRecordatorio);
        }

        public int Guardar(Recordatorio model)
        {
            return repositorio.Guardar(model);
        }

        public int Modificar(Recordatorio model)
        {
            return repositorio.Modificar(model);
        }

        public void Eliminar(int idRecordatorio)
        {
            repositorio.Eliminar(idRecordatorio);
        }
    }
}