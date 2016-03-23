using Salus.Infra.IoC;
using Salus.Model;
using Salus.Model.Entidades;
using Salus.Model.Repositorios;
using System.Collections.Generic;
using System.Web.Http;

namespace Web.ApiControllers
{
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

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}