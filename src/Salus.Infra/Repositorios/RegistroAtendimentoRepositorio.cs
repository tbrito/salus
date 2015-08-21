using System.Collections.Generic;

namespace Salus.Infra.Repositorios
{
    using Data;
    using Model.Entidades;
    using Model.Repositorios;

    public class RegistroAtendimentoRepositorio : Repositorio<RegistroAtendimento>, IRegistroAdentimentoRepositorio
    {
        public bool JaExisteRegistroEmAberto(RegistroAtendimento registroAtendimento)
        {
            throw new System.NotImplementedException();
        }

        public RegistroAtendimento ObterRegistroEntradaDoUsuario(Usuario usuario)
        {
            throw new System.NotImplementedException();
        }

        public RegistroAtendimento ObterAtendimentoFinalizado()
        {
            throw new System.NotImplementedException();
        }

        public IList<RegistroAtendimento> ObterPorTipoAtendimentoEEspecialidade(Atendimento infantil, Especialidade ortopedia)
        {
            throw new System.NotImplementedException();
        }
    }
}