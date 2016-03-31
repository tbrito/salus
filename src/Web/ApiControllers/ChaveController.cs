namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class ChaveController : ApiController
    {
        private IChaveRepositorio chaveRepositorio;

        public ChaveController()
        {
            this.chaveRepositorio = InversionControl.Current.Resolve<IChaveRepositorio>();
        }

        [HttpGet]
        public IEnumerable<Chave> PorTipoDocumento(int id)
        {
            var chaves = this.chaveRepositorio.ObterPorTipoDocumentoId(id);
            return chaves as IEnumerable<Chave>;
        }

        [HttpGet]
        public Chave ObterPorId(int id)
        {
            var chave = this.chaveRepositorio.ObterPorIdComTipoDocumento(id);
            return chave;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Salvar([FromBody]ChaveViewModel chaveViewModel)
        {
            Chave chave = null;

            if (chaveViewModel.Id == 0)
            {
                chave = new Chave();
                chave.TipoDocumento = new TipoDocumento { Id = chaveViewModel.TipoDocumentoId };
            }
            else
            {
                chave = this.chaveRepositorio.ObterPorId(chaveViewModel.Id);
            }

            chave.Nome = chaveViewModel.Nome;
            chave.Obrigatorio = chaveViewModel.Obrigatorio;
            chave.TipoDado = (TipoDado)chaveViewModel.TipoDado;
            chave.Mascara = chaveViewModel.Mascara;
            chave.Ativo = chaveViewModel.Ativo;
            chave.ItensLista = chaveViewModel.ItensLista;
            
            this.chaveRepositorio.Salvar(chave);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public void Excluir(int id)
        {
            this.chaveRepositorio.MarcarComoInativo(id);
        }
    }
}