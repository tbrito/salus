namespace Web.Controllers
{
    using Extensions;
    using Salus.Infra.IoC;
    using Salus.Model.Repositorios;
    using Salus.Model.UI;
    using System.Web.Mvc;

    public class ChaveConfigController : Controller
    {
        private ITipoDocumentoRepositorio tipoDocumentoRepositorio;

        public ChaveConfigController()
        {
            this.tipoDocumentoRepositorio = InversionControl.Current.Resolve<ITipoDocumentoRepositorio>();
        }

        [UrlRoute("ChaveConfig/{tipodocumentoId}")]
        public ActionResult Index(int tipodocumentoId)
        {
            var tipoDocumento = this.tipoDocumentoRepositorio.ObterPorId(tipodocumentoId);
            var chave = new ChaveViewModel { TipoDocumentoId = tipodocumentoId, TipoDocumentoNome = tipoDocumento.Nome };

            return PartialView(chave);
        }

        [UrlRoute("ChaveConfig/Editar/{chaveid}")]
        public ActionResult Editar(int chaveid)
        {
            var chave = new ChaveViewModel { Id = chaveid };
            return PartialView(chave);
        }
    }
}