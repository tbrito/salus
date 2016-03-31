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
        private IChaveRepositorio chaveRepositorio;

        public ChaveConfigController()
        {
            this.tipoDocumentoRepositorio = InversionControl.Current.Resolve<ITipoDocumentoRepositorio>();
            this.chaveRepositorio = InversionControl.Current.Resolve<IChaveRepositorio>();
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
            var chave = this.chaveRepositorio.ObterPorIdComTipoDocumento(chaveid);
            var chaveView = new ChaveViewModel { TipoDocumentoId = chave.TipoDocumento.Id, Id = chaveid };
            return PartialView(chaveView);
        }

        [UrlRoute("ChaveConfig/Adicionar/{tipodocumentoId}")]
        public ActionResult Adicionar(int tipodocumentoId)
        {
            var chave = new ChaveViewModel { TipoDocumentoId = tipodocumentoId, Id = 0 };
            return PartialView("Editar", chave);
        }
    }
}