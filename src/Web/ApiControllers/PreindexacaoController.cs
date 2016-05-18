namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class PreindexacaoController : ApiController
    {
        private IDocumentoRepositorio documentoRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;
        
        public PreindexacaoController()
        {
            this.documentoRepositorio = InversionControl.Current.Resolve<IDocumentoRepositorio>();
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
        }

        [HttpGet]
        public IEnumerable<Documento> Get()
        {
            var documentos = this.documentoRepositorio
                .ObterPreIndexadosPorUsuario(this.sessaoDoUsuario.UsuarioAtual);
            
            return documentos as IEnumerable<Documento>;
        }

        [HttpGet]
        public Documento ObterPorId(int id)
        {
            var documento = this.documentoRepositorio
                .ObterPorIdComTipoDocumentoEIndexacoes(id);

            return documento;
        }

        [HttpPost]
        public void Salvar([FromBody]UsuarioViewModel usuarioViewModel)
        {
        }
        
        [HttpDelete]
        public void Excluir(int id)
        {
        }
    }
}