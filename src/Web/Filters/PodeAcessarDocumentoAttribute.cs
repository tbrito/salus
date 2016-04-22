using Salus.Infra.IoC;
using Salus.Model.Repositorios;
using Salus.Model.Servicos;
using System;
using System.Web.Mvc;

namespace Web.Filters
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PodeAcessarDocumentoAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private ISessaoDoUsuario sessaoDoUsuario;
        private IAcessoDocumentoRepositorio acessoDocumentoRepositorio;
        private IDocumentoRepositorio documentoRepositorio;
        private AutorizaVisualizacaoDocumento autorizaVisualizacaoDocumento;

        public PodeAcessarDocumentoAttribute()
        {
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
            this.acessoDocumentoRepositorio = InversionControl.Current.Resolve<IAcessoDocumentoRepositorio>();
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var documentoId = filterContext.HttpContext.Request.Url.Segments[2];
            var acessos = this.acessoDocumentoRepositorio.ObterDoUsuario(this.sessaoDoUsuario.UsuarioAtual);
            var documento = this.documentoRepositorio.ObterPorId(Convert.ToInt32(documentoId));

            var podeAcessar = this.autorizaVisualizacaoDocumento.Executar(acessos, documento);

            if (podeAcessar == false)
            {
                filterContext.Result = new RedirectResult("/NaoAutorizado/Index");
            }
        }
    }
}