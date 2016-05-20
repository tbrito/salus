namespace Web.ApiControllers
{
    using Salus.Infra;
    using Salus.Infra.IoC;
    using Salus.Infra.Logs;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    public class VersaoDocumentoController : ApiController
    {
        private readonly StorageServico storageServico;
        private readonly ISessaoDoUsuario sessaoDoUsuario;
        private readonly VersaoDocumentoServico versaoDocumentoServico;
        private readonly IVersaoDocumentoRepositorio versaoDocumentoRepositorio;

        public VersaoDocumentoController()
        {
            this.versaoDocumentoRepositorio = InversionControl.Current.Resolve<IVersaoDocumentoRepositorio>();
            this.versaoDocumentoServico = InversionControl.Current.Resolve<VersaoDocumentoServico>();
            this.storageServico = InversionControl.Current.Resolve<StorageServico>();
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
        }

        // GET api/<controller>
        [HttpGet]
        public IEnumerable<VersaoDocumento> ObterPorDocumento(int id)
        {
            var versoes = this.versaoDocumentoRepositorio
                .ObterDoDocumento(id);

            Log.App.Info("Versões do documento:: " + versoes.Count);
            
            return versoes as IEnumerable<VersaoDocumento>;
        }

        [HttpGet]
        public IHttpActionResult Checkout(int id)
        {
            this.versaoDocumentoServico.Checkout(id);

            var versoes = this.versaoDocumentoRepositorio.ObterDoDocumento(id);

            var caminho = "1";

            if (versoes.Count > 0)
            {
                var ultimaVersao = versoes.OrderBy(x => x.CriadoEm).LastOrDefault().Versao;
                caminho = this.storageServico.Obter("[documento]" + id + "[versao]" + ultimaVersao);
            }
            else
            {
                caminho = this.storageServico.Obter("[documento]" + id);
            }

            var relativo = caminho.Replace(Aplicacao.Caminho, string.Empty);

            return Ok(new { UrlDocumento = relativo });
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}