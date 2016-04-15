namespace Web.ApiControllers
{
    using Salus.Model.Entidades;
    using System.Collections.Generic;
    using System.Web.Http;

    public class FuncionalidadeController : ApiController
    {
        public IEnumerable<Funcionalidade> Get()
        {
            return Funcionalidade.GetAll() as IEnumerable<Funcionalidade>; ;
        }
        
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}