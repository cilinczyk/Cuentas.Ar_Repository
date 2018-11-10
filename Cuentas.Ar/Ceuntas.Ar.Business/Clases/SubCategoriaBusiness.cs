using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class SubCategoriaBusiness
    {
        private readonly SubCategoriaRepository repositorio;

        public SubCategoriaBusiness()
        {
            this.repositorio = new SubCategoriaRepository();
        }

        public List<SubCategoria> Listar(int idUsuario)
        {
            return repositorio.Listar(idUsuario);
        }

        public List<SubCategoria> Listar(int idUsuario, int idCategoria)
        {
            return repositorio.Listar(idUsuario, idCategoria);
        }

        public SubCategoria Obtener(int idSubCategoria)
        {
            return repositorio.Obtener(idSubCategoria);
        }

        public int Guardar(SubCategoria model)
        {
            return repositorio.Guardar(model);
        }

        public int Modificar(SubCategoria model)
        {
            return repositorio.Modificar(model);
        }

        public void Eliminar(int idSubCateogria)
        {
            repositorio.Eliminar(idSubCateogria);
        }
    }
}