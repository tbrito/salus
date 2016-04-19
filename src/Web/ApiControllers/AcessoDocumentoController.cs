namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.UI;
    using System.Linq;
    using System.Web.Http;

    public class AcessoDocumentoController : ApiController
    {
        private IAcessoDocumentoRepositorio acessoDocumentoRepositorio;
        private ITipoDocumentoRepositorio tipoDocumentoRepositorio;

        public AcessoDocumentoController()
        {
            this.acessoDocumentoRepositorio = InversionControl.Current.Resolve<IAcessoDocumentoRepositorio>();
            this.tipoDocumentoRepositorio = InversionControl.Current.Resolve<ITipoDocumentoRepositorio>();
        }

        [HttpGet]
        public AcessoDocumentoViewModel ObterPor(int atorId = 0, int papelId = 0)
        {
            var acessos = this.acessoDocumentoRepositorio
                .ObterPorPapelComAtorId(papelId, atorId);

            var acessoViewModel = new AcessoDocumentoViewModel();
            acessoViewModel.AtorId = atorId;
            acessoViewModel.PapelId = papelId;

            foreach (var tipoDocumento in tiposDocumentos)
            {
                var tipoPermitidoViewModel = new TipoPermitidoViewModel();
                tipoPermitidoViewModel.Id = tipoDocumento.Id;
                tipoPermitidoViewModel.Marcado = acessos.Any(x => x.TipoDocumento.Id == tipoDocumento.Id);
                tipoPermitidoViewModel.Nome = tipoDocumento.Nome;
                acessoViewModel.TiposDocumentos.Add(tipoPermitidoViewModel);
            }            
            
            return acessoViewModel;
        }

        [HttpPost]
        public void Salvar([FromBody]AcessoDocumentoViewModel acessosViewModel)
        {
            this.acessoDocumentoRepositorio
                .ApagarAcessosDoAtor(acessosViewModel.PapelId, acessosViewModel.AtorId);

            foreach (var tipoDocumento in acessosViewModel.TiposDocumentos)
            {
                if (tipoDocumento.Marcado == false)
                {
                    continue;
                }

                var acesso = new AcessoDocumento
                {
                    AtorId = acessosViewModel.AtorId,
                    Papel = Papel.FromInt32(acessosViewModel.PapelId),
                    TipoDocumento = tipoDocumento
                };

                this.acessoDocumentoRepositorio.Salvar(acesso);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}