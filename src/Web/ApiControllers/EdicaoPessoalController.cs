namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class EdicaoPessoalController : ApiController
    {
        private IPerfilRepositorio perfilRepositorio;
        private IUsuarioRepositorio usuarioRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;
        private HashString hashString;

        public EdicaoPessoalController()
        {
            this.perfilRepositorio = InversionControl.Current.Resolve<IPerfilRepositorio>();
            this.usuarioRepositorio = InversionControl.Current.Resolve<IUsuarioRepositorio>();
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
            this.hashString = InversionControl.Current.Resolve<HashString>();
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
        public string SalvarSenha([FromBody]EditarPerfilViewModel editarPerfil)
        {
            if (string.IsNullOrEmpty(editarPerfil.NovaSenha))
            {
                return string.Empty;
            }

            var novaSenha = this.hashString.Do(editarPerfil.NovaSenha);

           this.usuarioRepositorio.SalvarSenha(
                this.sessaoDoUsuario.UsuarioAtual.Id,
                novaSenha);

            return "ok";
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