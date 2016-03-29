using Salus.Model.Entidades;
using Salus.Model.Repositorios;

namespace Salus.Model.Servicos
{
    public class LogarAcaoDoSistema
    {
        private ITrilhaRepositorio trilhaRepositorio;

        public LogarAcaoDoSistema(ITrilhaRepositorio trilhaRepositorio)
        {
            this.trilhaRepositorio = trilhaRepositorio;
        }

        public void Execute(Trilha trilha)
        {
            this.trilhaRepositorio.Salvar(trilha);
        }
    }
}