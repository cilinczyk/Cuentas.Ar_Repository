using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<Categoria> Listar()
        {
            return repositorio.Listar();
        }
    }
}