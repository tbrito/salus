﻿using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface ITipoDocumentoRepositorio : IRepositorio<TipoDocumento>
    {
        IList<TipoDocumento> ObterTodosClassificaveis(Usuario usuario);

        IList<TipoDocumento> ObterTodosGrupos(Usuario usuarioAtual);

        IList<TipoDocumento> ObterTodosParaIndexar(Usuario usuarioAtual);

        TipoDocumento ObterPorIdComParents(int id);

        void MarcarComoInativo(int id);

        void Ativar(int id);
    }
}