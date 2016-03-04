namespace Salus.Infra.WebUI
{
    using System;
    using Salus.Model.Entidades;
    using System.Web;
    using Repositorios;
    using System.Web.Security;
    public class SessaoDoUsuario
    {
        private UsuarioRepositorio usuarioRepositorio;

        public SessaoDoUsuario() : this(new UsuarioRepositorio())
        {
        }

        public SessaoDoUsuario(UsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public Usuario UsuarioAtual
        {
            get
            {
                return this.ObterUsuarioLogado();
            }
        }

        private Usuario ObterUsuarioLogado()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var nomeUsuario = HttpContext.Current.User.Identity.Name;
                var usuario = this.usuarioRepositorio.ProcurarPorNome(nomeUsuario);

                if (usuario == null)
                {
                    FormsAuthentication.SignOut();
                }

                return usuario;
            }

            return null;
        }
    }
}