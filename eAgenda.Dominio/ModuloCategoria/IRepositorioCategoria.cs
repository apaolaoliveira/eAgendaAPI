using eAgenda.Dominio.Compartilhado;
using System;
using System.Collections.Generic;

namespace eAgenda.Dominio.ModuloCategoria
{
    public interface IRepositorioCategoria : IRepositorio<Categoria>
    {
        List<Categoria> SelecionarMuitos(List<Guid> idsCategoriasSelecionadas);
    }
}
