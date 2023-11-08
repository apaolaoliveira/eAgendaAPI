using eAgenda.Dominio;
using eAgenda.Dominio.ModuloContato;
using FluentResults;
using Serilog;
using System;
using System.Collections.Generic;

namespace eAgenda.Aplicacao.ModuloContato
{
    public class ServicoContato : ServicoApiBase<Contato, ValidadorContato>, IServicoApiBase<Contato>
    {
        private IRepositorioContato repositorioContato;
        private IContextoPersistencia contextoPersistencia;

        public ServicoContato(IRepositorioContato repositorioContato,
                             IContextoPersistencia contexto)
        {
            this.repositorioContato = repositorioContato;
            this.contextoPersistencia = contexto;
        }

        public Result<Contato> Inserir(Contato contato)
        {
            Result resultado = Validar(contato);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioContato.Inserir(contato);

            contextoPersistencia.GravarDados();

            return Result.Ok(contato);
        }

        public Result<Contato> Editar(Contato contato)
        {
            var resultado = Validar(contato);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioContato.Editar(contato);

            contextoPersistencia.GravarDados();

            return Result.Ok(contato);
        }

        public Result Excluir(Guid id)
        {
            var contatoResult = SelecionarPorId(id);

            if (contatoResult.IsSuccess)
                return Excluir(contatoResult.Value);

            return Result.Fail(contatoResult.Errors);
        }

        public Result Excluir(Contato contato)
        {
            repositorioContato.Excluir(contato);

            contextoPersistencia.GravarDados();

            return Result.Ok();
        }

        public Result<List<Contato>> SelecionarTodos(StatusFavoritoEnum statusFavorito)
        {
            var contatos = repositorioContato.SelecionarTodos(statusFavorito);

            return Result.Ok(contatos);
        }

        public Result<Contato> SelecionarPorId(Guid id)
        {
            var contato = repositorioContato.SelecionarPorId(id);

            if (contato == null)
            {
                Log.Logger.Warning("Contato {ContatoId} não encontrado", id);

                return Result.Fail($"Contato {id} não encontrado");
            }

            return Result.Ok(contato);
        }

        public Result<Contato> ConfigurarFavoritos(Contato contato)
        {
            contato.ConfigurarFavorito();

            repositorioContato.Editar(contato);

            contextoPersistencia.GravarDados();

            return Result.Ok(contato);
        }
    }
}
