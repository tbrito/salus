namespace Salus.Model.Servicos
{
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System;

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

        public void Execute(TipoTrilha tipoTrilha, string titulo, string mensagem)
        {
            this.Execute(
                tipoTrilha,
                titulo,
                mensagem,
                this.sessaoDoUsuario.UsuarioAtual);
        }

        public void Execute(TipoTrilha tipoTrilha, string titulo, string mensagem, Usuario usuario)
        {
            try
            {
                var trilha = new Trilha
                {
                    Data = DateTime.Now,
                    Descricao = titulo,
                    Tipo = tipoTrilha,
                    Usuario = usuario,
                    Recurso = mensagem
                };

                this.trilhaRepositorio.Salvar(trilha);
            }
            catch (Exception ex)
            {
            }
        }
    }
}