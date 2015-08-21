namespace Salus.Model.Repositorios
{
    using Data;
    using Entidades;

    public interface IRegistroAdentimentoRepositorio : IRepositorio<RegistroAtendimento>
    {
        bool JaExisteEntrada(RegistroAtendimento registroAtendimento);

        RegistroAtendimento ObterRegistroEntradaDoUsuario(Usuario usuario);
    }
}