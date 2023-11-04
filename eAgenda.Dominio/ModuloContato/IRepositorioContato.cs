using eAgenda.Dominio.Compartilhado;
using System.Collections.Generic;
using System;

namespace eAgenda.Dominio.ModuloContato
{
    public interface IRepositorioContato : IRepositorio<Contato>
    {
        List<Contato> SelecionarTodos(StatusFavoritoEnum statusFavorito);
    }
}
