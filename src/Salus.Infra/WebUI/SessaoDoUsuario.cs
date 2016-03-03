namespace Salus.Infra.WebUI
{
    using Salus.Model.Entidades;

    public class SessaoDoUsuario
    {
        public SessaoDoUsuario()
        {
            this.UsuarioAtual = new Usuario
            {
                Id = 132
            };
        }

        public Usuario UsuarioAtual
        {
            get;
            set;
        }
    }
}