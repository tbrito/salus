namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class AreaController : ApiController
    {
        private IAreaRepositorio areaRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;

        public AreaController()
        {
            this.areaRepositorio = InversionControl.Current.Resolve<IAreaRepositorio>();
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
        }

        public IEnumerable<AreaViewModel> Get()
        {
            var areas = this.areaRepositorio
                .ObterTodosAtivos(this.sessaoDoUsuario.UsuarioAtual);

            var areasViewModel = new List<AreaViewModel>();

            foreach (var area in areas)
            {
                var viewModel = new AreaViewModel
                {
                    Id = area.Id,
                    Abreviacao = area.Abreviacao,
                    Ativo = area.Ativo,
                    Nome = area.Nome,
                    Segura = area.Segura,
                    Parent = area.Parent
                };

                areasViewModel.Add(viewModel);
            }
            
            return areasViewModel as IEnumerable<AreaViewModel>; ;
        }

        [HttpGet]
        public Area ObterPorId(int id)
        {
            var area = this.areaRepositorio.ObterPorIdComParents(id);
            return area;
        }

        [HttpPost]
        public void Salvar([FromBody]AreaViewModel areaViewModel)
        {
            Area area = null;

            if (areaViewModel.Id == 0)
            {
                area = new Area();
            }
            else
            {
                area = this.areaRepositorio.ObterPorId(areaViewModel.Id);
            }

            area.Ativo = areaViewModel.Ativo;
            area.Nome = areaViewModel.Nome;
            area.Abreviacao = areaViewModel.Abreviacao;
            area.Segura = areaViewModel.Segura;

            if (areaViewModel.Parent != null)
            {
                area.Parent = new Area { Id = areaViewModel.Parent.Id };
            }

            this.areaRepositorio.Salvar(area);
        }

        [HttpPut]
        public void Ativar(int id, [FromBody]string value)
        {
            this.areaRepositorio.Reativar(id);
        }

        [HttpDelete]
        public void Excluir(int id)
        {
            this.areaRepositorio.MarcarComoInativo(id);
        }
    }
}