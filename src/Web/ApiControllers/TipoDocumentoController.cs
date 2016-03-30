namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System.Collections.Generic;
    using System.Web.Http;

    public class TipoDocumentoController : ApiController
    {
        private ITipoDocumentoRepositorio tipoDocumentoRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;

        public TipoDocumentoController()
        {
            this.tipoDocumentoRepositorio = InversionControl.Current.Resolve<ITipoDocumentoRepositorio>();
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
        }

        // GET api/<controller>
        public IEnumerable<TipoDocumento> Get()
        {
            var tiposDocumentos = this.tipoDocumentoRepositorio
                .ObterTodosClassificaveis(this.sessaoDoUsuario.UsuarioAtual);
           
            return tiposDocumentos as IEnumerable<TipoDocumento>;
        }

        // GET api/<controller>/5
        public TipoDocumento Get(int id)
        {
            var tipoDocumento = this.tipoDocumentoRepositorio.ObterPorIdComParents(id);
            return tipoDocumento;
        }

        // POST api/<controller>
        [HttpPost]
        public void Salvar([FromBody]TipoDocumentoViewModel tipoDocumentoView)
        {
            TipoDocumento tipoDocumento = null;

            if (tipoDocumentoView.Id == 0)
            {
                tipoDocumento = new TipoDocumento();
            }
            else
            {
                tipoDocumento = this.tipoDocumentoRepositorio.ObterPorId(tipoDocumentoView.Id);
            }

            tipoDocumento.Ativo = tipoDocumentoView.Ativo;
            tipoDocumento.Nome = tipoDocumentoView.Nome;
            tipoDocumento.EhPasta = tipoDocumentoView.EhPasta;
            tipoDocumento.Parent = new TipoDocumento { Id = tipoDocumentoView.ParentId };

            this.tipoDocumentoRepositorio.Salvar(tipoDocumento);
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

    public class TipoDocumentoViewModel
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public bool EhPasta { get; set; }
        public string Nome { get; set; }
        public int ParentId { get; set; }
    }
}