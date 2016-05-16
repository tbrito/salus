namespace Salus.Infra.WebUI
{
    using System;
    using Salus.Model.Entidades;
    using System.Web;
    using Repositorios;
    using System.Web.Security;
    using Model.Repositorios;
    using IoC;
    using System.Threading;
    public class SessaoDoUsuario : ISessaoDoUsuario
    {
        private IUsuarioRepositorio usuarioRepositorio;
        
        public SessaoDoUsuario()
        {
            this.usuarioRepositorio = InversionControl.Current.Resolve<IUsuarioRepositorio>();
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
            if (HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var nomeUsuario = HttpContext.Current.User.Identity.Name;
                var usuario = this.usuarioRepositorio.ProcurarPorNome(nomeUsuario);

                if (usuario == null)
                {
                    FormsAuthentication.SignOut();
                }

                return usuario;
            }

            FormsAuthentication.SignOut();

            return null;
        }
    }
}