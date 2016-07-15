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

        [HttpGet]
        public IEnumerable<TipoDocumentoViewModel> Todos(int id = 0)
        {
            var tiposDocumentos = this.tipoDocumentoRepositorio
                .ObterTodosClassificaveis(this.sessaoDoUsuario.UsuarioAtual);

            var models = new List<TipoDocumentoViewModel>();

            foreach (var tipoDocumento in tiposDocumentos)
            {
                var model = TipoDocumentoViewModel.Criar(tipoDocumento);
                models.Add(model);
            }

            return models as IEnumerable<TipoDocumentoViewModel>;
        }

        [HttpGet]
        public IEnumerable<TipoDocumentoViewModel> ParaIndexar(int id = 0)
        {
            var tiposDocumentos = this.tipoDocumentoRepositorio
                .ObterTodosParaIndexar(this.sessaoDoUsuario.UsuarioAtual);

            var models = new List<TipoDocumentoViewModel>();

            foreach (var tipoDocumento in tiposDocumentos)
            {
                var model = TipoDocumentoViewModel.Criar(tipoDocumento);
                models.Add(model);
            }

            return models as IEnumerable<TipoDocumentoViewModel>;
        }

        [HttpGet]
        public TipoDocumentoViewModel ObterPorId(int id)
        {
            var tipoDocumento = this.tipoDocumentoRepositorio.ObterPorIdComParents(id);

            var model = TipoDocumentoViewModel.Criar(tipoDocumento);

            return model;
        }

        [HttpPost]
        public TipoDocumentoViewModel Salvar([FromBody]TipoDocumentoViewModel tipoDocumentoView)
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

            if (tipoDocumentoView.Parent != null)
            {
                tipoDocumento.Parent = new TipoDocumento { Id = tipoDocumentoView.Parent.Id };
            }

            this.tipoDocumentoRepositorio.Salvar(tipoDocumento);
            tipoDocumentoView.Id = tipoDocumento.Id;

            return tipoDocumentoView;
        }

        [HttpPut]
        public void Ativar(int id, [FromBody]string value)
        {
            this.tipoDocumentoRepositorio.Ativar(id);
        }

        [HttpDelete]
        public void Excluir(int id)
        {
            this.tipoDocumentoRepositorio.MarcarComoInativo(id);
        }
    }
}