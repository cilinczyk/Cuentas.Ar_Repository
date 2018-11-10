using System.Collections.Generic;
using Cuentas.Ar.Entities;
using Cuentas.Ar.Repository;

namespace Cuentas.Ar.Business
{
    public class CategoriaBusiness
    {
        private readonly CategoriaRepository repositorio;

        public CategoriaBusiness()
        {
            this.repositorio = new CategoriaRepository();
        }

        public List<Categoria> Listar(int idUsuario)
        {
            return repositorio.Listar(idUsuario);
        }

        public List<Categoria> Listar(int idUsuario, int idTipoRegistro)
        {
            return repositorio.Listar(idUsuario, idTipoRegistro);
        }

        public Categoria Obtener(int idCategoria)
        {
            return repositorio.Obtener(idCategoria);
        }

        public int Guardar(Categoria model)
        {
            return repositorio.Guardar(model);
        }

        public int Modificar(Categoria model)
        {
            return repositorio.Modificar(model);
        }

        public void Eliminar(int idCateogria)
        {
            repositorio.Eliminar(idCateogria);
        }
    }
}