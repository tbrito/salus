using System;
using Salus.Model.UI;
using Salus.Model.Repositorios;
using Salus.Model.Entidades;

namespace Salus.Model.Servicos
{
    public class SalvarUsuarioServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly HashString hashString;

        public SalvarUsuarioServico(
            IUsuarioRepositorio usuarioRepositorio,
            HashString hashString)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.hashString = hashString;
        }           

        public void Execute(UsuarioViewModel usuarioViewModel)
        {
            Usuario usuario = null;

            if (usuarioViewModel.Id == 0)
            {
                usuario = new Usuario();
                usuario.Senha = this.hashString.Do(usuarioViewModel.Senha);
            }
            else
            {
                usuario = this.usuarioRepositorio.ObterPorId(usuarioViewModel.Id);
            }

            usuario.Ativo = usuarioViewModel.Ativo;
            usuario.Login = usuarioViewModel.Nome;
            usuario.Email = usuarioViewModel.Email;
            usuario.Expira = usuarioViewModel.Expira;

            usuario.ExpiraEm = usuarioViewModel.Expira ?
                usuario.ExpiraEm = usuarioViewModel.ExpiraEm :
                usuario.ExpiraEm = null;

            if (usuarioViewModel.Area != null)
            {
                usuario.Area = new Area { Id = usuarioViewModel.Area.Id };
            }

            if (usuarioViewModel.Perfil != null)
            {
                usuario.Perfil = new Perfil { Id = usuarioViewModel.Perfil.Id };
            }

            this.usuarioRepositorio.Salvar(usuario);
        }
    }
}