namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System.Collections.Generic;
    using System.Web.Http;

    public class TrilhaController : ApiController
    {
        private ITrilhaRepositorio trilhaRepositorio;
        
        public TrilhaController()
        {
            this.trilhaRepositorio = InversionControl.Current.Resolve<ITrilhaRepositorio>();
        }

        public IEnumerable<Trilha> Get()
        {
            var trilhas = this.trilhaRepositorio.ObterTodosComUsuario();
           
            return trilhas as IEnumerable<Trilha>;
        }
        
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}