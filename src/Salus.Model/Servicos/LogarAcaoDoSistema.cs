using Salus.Model.Entidades;
using Salus.Model.Repositorios;
using System;
namespace Salus.Model.Servicos
{
    public class LogarAcaoDoSistema
    {
        private ISessaoDoUsuario sessaoDoUsuario;
        private ITrilhaRepositorio trilhaRepositorio;

        public LogarAcaoDoSistema(
            ITrilhaRepositorio trilhaRepositorio,
            ISessaoDoUsuario sessaoDoUsuario)
        {
            this.trilhaRepositorio = trilhaRepositorio;
            this.sessaoDoUsuario = sessaoDoUsuario;
        }

        public void Execute(TipoTrilha tipoTrilha, string mensagem)
        {
            try
            {
                var trilha = new Trilha
                {
                    Data = DateTime.Now,
                    Descricao = mensagem,
                    Tipo = tipoTrilha,
                    Usuario = this.sessaoDoUsuario.UsuarioAtual,
                    Recurso = "Recurso"
                };

                this.trilhaRepositorio.Salvar(trilha);
            }
            catch (Exception ex)
            {
            }
        }
    }
}