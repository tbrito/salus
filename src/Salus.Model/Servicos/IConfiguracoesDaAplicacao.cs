namespace Salus.Model.Servicos
{
    public interface IConfiguracoesDaAplicacao
    {
        int MaximoResultadoPorPagina { get; }

        int ResultadoMaximoConsulta { get; }

        string CaminhoIndicePesquisa();
    }
}
