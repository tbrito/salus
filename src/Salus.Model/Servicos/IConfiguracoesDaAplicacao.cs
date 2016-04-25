namespace Salus.Model.Servicos
{
    public interface IConfiguracoesDaAplicacao
    {
        int MaximoResultadoPorPagina { get; set; }

        int ResultadoMaximoConsulta { get; set; }

        string CaminhoIndicePesquisa();
    }
}
