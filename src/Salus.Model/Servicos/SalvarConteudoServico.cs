using Salus.Model.Entidades;
using Salus.Model.UI;
using System;
using System.Collections.Generic;

namespace Salus.Model.Servicos
{
    public class SalvarConteudoServico
    {
        public IList<Documento> Executar(IList<FileViewModel> documentos)
        {
            return new List<Documento>
            {
                new Documento() { Id = 1, DataCriacao = DateTime.Parse("01/02/1995") },
                new Documento() { Id = 2, DataCriacao = DateTime.Parse("01/03/1996") },
                new Documento() { Id = 3, DataCriacao = DateTime.Parse("01/04/1997") },
            };
        }
    }
}