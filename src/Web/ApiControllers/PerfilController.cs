namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class PerfilController : ApiController
    {
        private IPerfilRepositorio perfilRepositorio;

        public PerfilController()
        {
            this.perfilRepositorio = InversionControl.Current.Resolve<IPerfilRepositorio>();
        }

        public IEnumerable<Perfil> Get()
        {
            var perfis = this.perfilRepositorio.ObterTodos();
           
            return perfis as IEnumerable<Perfil>;
        }

        [HttpGet]
        public Perfil ObterPorId(int id)
        {
            var perfil = this.perfilRepositorio.ObterPorId(id);
            return perfil;
        }

        [HttpPost]
        public void Salvar([FromBody]Perfil perfil)
        {
            Perfil perfilMontado = null;

            if (perfil.Id == 0)
            {
                perfilMontado = new Perfil();
            }
            else
            {
                perfilMontado = this.perfilRepositorio.ObterPorId(perfil.Id);
            }

            perfilMontado.Ativo = perfil.Ativo;
            perfilMontado.Nome = perfil.Nome;

            this.perfilRepositorio.Salvar(perfilMontado);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public void Excluir(int id)
        {
            this.perfilRepositorio.MarcarComoInativo(id);
        }
    }
}