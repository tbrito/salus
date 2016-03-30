namespace Salus.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class ChaveRepositorio : Repositorio<Chave>, IChaveRepositorio
    {
        public void MarcarComoInativo(int id)
        {
            this.Sessao
              .CreateQuery("update Chave set Ativo = false where Id = :id")
              .SetInt32("id", id)
              .ExecuteUpdate();
        }

        public Chave ObterPorIdComTipoDocumento(int id)
        {
            return this.Sessao.QueryOver<Chave>()
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.TipoDocumento.Parent).Eager
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public IList<Chave> ObterPorTipoDocumentoId(int id)
        {
            return this.Sessao.QueryOver<Chave>()
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.TipoDocumento.Parent).Eager
                .Where(x => x.TipoDocumento.Id == id)
                .List();
        }
    }
}