namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class UsuarioController : ApiController
    {
        private IUsuarioRepositorio usuarioRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;

        public UsuarioController()
        {
            this.usuarioRepositorio = InversionControl.Current.Resolve<IUsuarioRepositorio>();
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
        }

        public IEnumerable<Usuario> Get()
        {
            var usuarios = this.usuarioRepositorio
                .ObterTodosComAreaEPerfil();
           
            return usuarios as IEnumerable<Usuario>;
        }

        [HttpGet]
        public Usuario ObterPorId(int id)
        {
            var usuario = this.usuarioRepositorio.ObterPorIdComParents(id);
            return usuario;
        }

        [HttpPost]
        public void Salvar([FromBody]UsuarioViewModel usuarioViewModel)
        {
            Usuario usuario = null;

            if (usuarioViewModel.Id == 0)
            {
                usuario = new Usuario();
            }
            else
            {
                usuario = this.usuarioRepositorio.ObterPorId(usuarioViewModel.Id);
            }

            usuario.Ativo = usuarioViewModel.Ativo;
            usuario.Nome = usuarioViewModel.Nome;
            usuario.Email = usuarioViewModel.Email;
            usuario.Senha = usuarioViewModel.Senha;
            usuario.Expira = usuarioViewModel.Expira;
            usuario.ExpiraEm = usuarioViewModel.ExpiraEm;

            if (usuarioViewModel.Area != null)
            {
                usuario.Area = new Area { Id = usuarioViewModel.Area.id };
            }

            if (usuarioViewModel.Perfil != null)
            {
                usuario.Perfil = new Perfil { Id = usuarioViewModel.Perfil.id };
            }

            this.usuarioRepositorio.Salvar(usuario);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public void Excluir(int id)
        {
            this.usuarioRepositorio.MarcarComoInativo(id);
        }
    }
}