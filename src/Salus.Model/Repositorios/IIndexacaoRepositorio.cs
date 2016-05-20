using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface IIndexacaoRepositorio : IRepositorio<Indexacao>
    {
        IList<Indexacao> ObterPorDocumento(Documento documento);

        void AtualizarValor(int id, string valor);
    }
}