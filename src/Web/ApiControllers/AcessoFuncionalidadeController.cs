namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.UI;
    using System.Linq;
    using System.Web.Http;

    public class AcessoFuncionalidadeController : ApiController
    {
        private IAcessoFuncionalidadeRepositorio acessoFuncionalidadeRepositorio;

        public AcessoFuncionalidadeController()
        {
            this.acessoFuncionalidadeRepositorio = InversionControl.Current.Resolve<IAcessoFuncionalidadeRepositorio>();
        }

        [HttpGet]
        public AcessoViewModel ObterPor(int atorId = 0, int papelId = 0)
        {
            var acessos = this.acessoFuncionalidadeRepositorio
                .ObterPorPapelComAtorId(papelId, atorId);

            var acessoViewModel = new AcessoViewModel();
            acessoViewModel.AtorId = atorId;
            acessoViewModel.PapelId = papelId;

            foreach (var funcionalidade in Funcionalidade.GetAll())
            {
                var funcionalidadeViewModel = new FuncionalidadeViewModel();
                funcionalidadeViewModel.Id = funcionalidade.Value;
                funcionalidadeViewModel.Marcado = acessos.Any(x => x.Funcionalidade == funcionalidade);
                funcionalidadeViewModel.Nome = funcionalidade.DisplayName;
                acessoViewModel.Funcionalidades.Add(funcionalidadeViewModel);
            }            
            
            return acessoViewModel;
        }

        [HttpPost]
        public void Salvar([FromBody]AcessoViewModel acessosViewModel)
        {
            this.acessoFuncionalidadeRepositorio
                .ApagarAcessosDoAtor(acessosViewModel.PapelId, acessosViewModel.AtorId);

            foreach (var funcionalidade in acessosViewModel.Funcionalidades)
            {
                if (funcionalidade.Marcado == false)
                {
                    continue;
                }

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