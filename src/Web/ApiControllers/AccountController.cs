namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Infra.Repositorios;
    using Salus.Model.Servicos;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Security;

    public class AccountController : ApiController
    {
        private UsuarioRepositorio usuarioRepositorio;
        private HashString hashString;

        public AccountController()
        {
            this.usuarioRepositorio = InversionControl.Current.Resolve<UsuarioRepositorio>();
            this.hashString = InversionControl.Current.Resolve<HashString>();
        }

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
            var senha = this.hashString.Do(value.Senha);
            var usuario = this.usuarioRepositorio.Procurar(value.UserName, senha);

            var login = new LoginViewModel();
            login.Autenticado = false;

            if (usuario != null)
            {
                login = new LoginViewModel
                {
                    UserName = usuario.Nome,
                    Senha = usuario.Senha,
                    Autenticado = true
                };

                FormsAuthentication.SetAuthCookie(usuario.Nome, true);
            }

            return login;
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