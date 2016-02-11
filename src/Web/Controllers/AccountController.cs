namespace Web.Controllers
{
    using Salus.Infra.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class AccountController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public LoginViewModel Post([FromBody]LoginViewModel value)
        {
            return new LoginViewModel { UserName = "eu", Senha = "não te interessa", Autenticado = false };
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