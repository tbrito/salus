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
        public IEnumerable<ChaveViewModel> PorTipoDocumento(int id)
        {
            var chaves = this.chaveRepositorio.ObterPorTipoDocumentoId(id);

            var models = new List<ChaveViewModel>();

            foreach (var chave in chaves)
            {
                var model = new ChaveViewModel
                {
                    Ativo = chave.Ativo,
                    Id = chave.Id,
                    ItensLista = chave.ItensLista,
                    Mascara = chave.Mascara,
                    Nome = chave.Nome,
                    Obrigatorio = chave.Obrigatorio,
                    TipoDado = chave.TipoDado.Value,
                    TipoDocumentoId = chave.TipoDocumento.Id,
                    TipoDocumentoNome = chave.TipoDocumento.Nome
                };

                models.Add(model);
            }

            return models as IEnumerable<ChaveViewModel>;
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
        public Chave Salvar([FromBody]ChaveViewModel chaveViewModel)
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
            chave.TipoDado = TipoDado.FromInt32(chaveViewModel.TipoDado);
            chave.Mascara = chaveViewModel.Mascara;
            chave.Ativo = chaveViewModel.Ativo;
            chave.ItensLista = chaveViewModel.ItensLista;
            
            this.chaveRepositorio.Salvar(chave);
            
            return chave;
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