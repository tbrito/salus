namespace Web.ApiControllers
{
    using Extensions;
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class AcessoFuncionalidadeController : ApiController
    {
        private IAcessoFuncionalidadeRepositorio acessoFuncionalidadeRepositorio;

        public AcessoFuncionalidadeController()
        {
            this.acessoFuncionalidadeRepositorio = InversionControl.Current.Resolve<IAcessoFuncionalidadeRepositorio>();
        }

        [HttpGet]
        public IEnumerable<AcessoFuncionalidade> ObterPor([FromUri] AcessoViewModel viewModel)
        {
            var acessos = this.acessoFuncionalidadeRepositorio
                .ObterPorPapelComAtorId(viewModel.PapelId, viewModel.AtorId);

            return acessos as IEnumerable<AcessoFuncionalidade>;
        }

        [HttpPost]
        public void Salvar([FromBody]AcessoViewModel acessosViewModel)
        {
            foreach (var funcionalidade in acessosViewModel.Funcionalidades)
            {
                var acesso = new AcessoFuncionalidade
                {
                    AtorId = acessosViewModel.AtorId,
                    Papel = Papel.FromInt32(acessosViewModel.PapelId),
                    Funcionalidade = Funcionalidade.FromInt32(funcionalidade.Id)
                };

                this.acessoFuncionalidadeRepositorio.Salvar(acesso);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}