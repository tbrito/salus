namespace Salus.Infra.Repositorios
{
    using Data;
    using Model.Entidades;
    using Model.Repositorios;

    public class RegistroAtendimentoRepositorio : Repositorio<RegistroAtendimento>, IRegistroAdentimentoRepositorio
    {
        public bool JaExisteEntrada(RegistroAtendimento registroAtendimento)
        {
            throw new System.NotImplementedException();
        }

        public RegistroAtendimento ObterRegistroEntradaDoUsuario(Usuario usuario)
        {
            throw new System.NotImplementedException();
        }
    }
}