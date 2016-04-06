namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.UI;
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

        public IEnumerable<TipoDocumento> Get()
        {
            var tiposDocumentos = this.tipoDocumentoRepositorio
                .ObterTodosClassificaveis(this.sessaoDoUsuario.UsuarioAtual);
           
            return tiposDocumentos as IEnumerable<TipoDocumento>;
        }

        [HttpGet]
        public TipoDocumento ObterPorId(int id)
        {
            var tipoDocumento = this.tipoDocumentoRepositorio.ObterPorIdComParents(id);
            return tipoDocumento;
        }

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
            tipoDocumento.EhPasta = false;
            tipoDocumento.Parent = new TipoDocumento { Id = tipoDocumentoView.Parent.Id };

            this.tipoDocumentoRepositorio.Salvar(tipoDocumento);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public void Excluir(int id)
        {
            this.tipoDocumentoRepositorio.MarcarComoInativo(id);
        }
    }
}