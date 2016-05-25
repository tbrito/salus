namespace Web.ApiControllers
{
    using Salus.Infra;
    using Salus.Infra.IoC;
    using Salus.Infra.Logs;
    using Salus.Infra.Repositorios;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using Salus.Model.UI;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Security;

    public class AccountController : ApiController
    {
        private UsuarioRepositorio usuarioRepositorio;
        private IAcessoFuncionalidadeRepositorio acessoFuncionalidadeRepositorio;
        private HashString hashString;
        private StorageServico storageServico;
        private LogarAcaoDoSistema logarAcaoSistema;

        public AccountController()
        {
            this.usuarioRepositorio = InversionControl.Current.Resolve<UsuarioRepositorio>();
            this.acessoFuncionalidadeRepositorio = InversionControl.Current.Resolve<IAcessoFuncionalidadeRepositorio>();
            this.hashString = InversionControl.Current.Resolve<HashString>();
            this.logarAcaoSistema = InversionControl.Current.Resolve<LogarAcaoDoSistema>();
            this.storageServico = InversionControl.Current.Resolve<StorageServico>();
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

        [HttpGet]
        public IHttpActionResult Logout(int id)
        {
            this.logarAcaoSistema.Execute(
                TipoTrilha.Alteracao,
                "Logout",
                "Saiu do sistema");

            FormsAuthentication.SignOut();
            return Ok(new { Mensagem = "Ok" });
        }

        // POST api/<controller>
        public LoginViewModel Post([FromBody]LoginViewModel value)
        {
            var senha = this.hashString.Do(value.Senha);
            var usuario = this.usuarioRepositorio.Procurar(value.Login, senha);

            var login = new LoginViewModel();
            login.Autenticado = false;
            
            if (usuario != null)
            {
                login = this.ObterUsuarioModel(usuario);
                this.CriarTicketDeAutenticacao(usuario, login);
                this.logarAcaoSistema.Execute(
                    TipoTrilha.Acesso, 
                    "Acesso ao Sistema", 
                    "Acessou o sistema",
                    usuario);
            }

            return login;
        }

        private void CriarTicketDeAutenticacao(
            Usuario usuario, 
            LoginViewModel login)
        {
            FormsAuthentication.SetAuthCookie(usuario.Login, true);

            var coockie = FormsAuthentication.GetAuthCookie(usuario.Login, true);
            coockie.Expires = DateTime.Now.AddMinutes(30);
            var decriptedCoockie = FormsAuthentication.Decrypt(coockie.Value);

            var ticket = new FormsAuthenticationTicket(
                decriptedCoockie.Version,
                decriptedCoockie.Name,
                decriptedCoockie.IssueDate,
                coockie.Expires,
                true,
                decriptedCoockie.UserData);

            coockie.Value = FormsAuthentication.Encrypt(ticket);
            HttpContext.Current.Response.Cookies.Add(coockie);
        }

        private LoginViewModel ObterUsuarioModel(Usuario usuario)
        {
            LoginViewModel login;

            var funcionalidades = this.FuncionalidadesPermitidas(usuario);
            var relativo = string.Empty;

            try
            {
                var imagem = this.storageServico.Obter("[usuario]" + usuario.Id.ToString());

                relativo = imagem
                    .Replace(Aplicacao.Caminho, string.Empty)
                    .Replace(@"\", "/");
            }
            catch (Exception)
            {
                relativo = string.Empty;
                Log.App.Info("Usuario Sem Avatar");
            }

            login = new LoginViewModel
            {
                Login = usuario.Login,
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Autenticado = true,
                Funcionalidades = funcionalidades,
                Avatar = relativo
            };

            return login;
        }

        private List<FuncionalidadeViewModel> FuncionalidadesPermitidas(Salus.Model.Entidades.Usuario usuario)
        {
            var acessosPermitidos = this.acessoFuncionalidadeRepositorio.ObterDoUsuario(usuario);
            var funcionalidades = new List<FuncionalidadeViewModel>();

            foreach (var acesso in acessosPermitidos)
            {
                var viewModel = new FuncionalidadeViewModel
                {
                    Id = acesso.Funcionalidade.Value,
                    Nome = acesso.Funcionalidade.DisplayName,
                    Marcado = true
                };

                funcionalidades.Add(viewModel);
            }
            return funcionalidades;
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