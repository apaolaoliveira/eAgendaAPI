using eAgenda.Dominio;
using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Orm.ModuloCategoria
{
    public class RepositorioCategoriaOrm : RepositorioBase<Categoria>, IRepositorioCategoria
    {
        public RepositorioCategoriaOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }

        public List<Categoria> SelecionarMuitos(List<Guid> idsCategoriasSelecionadas)
        {
            return registros.Where(categoria => idsCategoriasSelecionadas.Contains(categoria.Id)).ToList();
        }

        public override Categoria SelecionarPorId(Guid id)
        {
            return registros
                .Include(x => x.Despesas)
                .SingleOrDefault(x => x.Id == id);
        }
    }
}
