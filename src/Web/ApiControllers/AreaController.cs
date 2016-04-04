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

        public IEnumerable<Area> Get()
        {
            var areas = this.areaRepositorio
                .ObterTodosAtivos(this.sessaoDoUsuario.UsuarioAtual);
           
            return areas as IEnumerable<Area>;
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
                area.Parent = new Area { Id = areaViewModel.Parent.id };
            }

            this.areaRepositorio.Salvar(area);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public void Excluir(int id)
        {
            this.areaRepositorio.MarcarComoInativo(id);
        }
    }
}