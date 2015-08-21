using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    using Data;
    using Entidades;

    public interface IRegistroAdentimentoRepositorio : IRepositorio<RegistroAtendimento>
    {
        bool JaExisteRegistroEmAberto(RegistroAtendimento registroAtendimento);

        RegistroAtendimento ObterRegistroEntradaDoUsuario(Usuario usuario);
        
        RegistroAtendimento ObterAtendimentoFinalizado();

        IList<RegistroAtendimento> ObterPorTipoAtendimentoEEspecialidade(Atendimento infantil, Especialidade ortopedia);
    }
}