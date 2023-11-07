using eAgenda.Dominio.Compartilhado;
using FluentResults;
using System.Collections.Generic;
using System;

namespace eAgenda.Aplicacao.Compartilhado
{
    public interface IServicoApiBase<Type> where Type : EntidadeBase<Type>
    {
        public virtual Result<Type> Inserir(Type registro) { return null; }
        public virtual Result<Type> Editar(Type registro) { return null; }
        public virtual Result Excluir(Type registro) { return null; }
        public virtual Result<List<Type>> SelecionarTodos() { return null; }
        public virtual Result<Type> SelecionarPorId(Guid id) { return null; }
    }
}
