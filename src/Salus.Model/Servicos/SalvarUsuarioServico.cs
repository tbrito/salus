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
        private readonly LogarAcaoDoSistema logarAcaoSistema;

        public SalvarUsuarioServico(
            IUsuarioRepositorio usuarioRepositorio,
            HashString hashString,
            LogarAcaoDoSistema logarAcaoSistema)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.hashString = hashString;
            this.logarAcaoSistema = logarAcaoSistema;
        }           

        public void Execute(UsuarioViewModel usuarioViewModel)
        {
            Usuario usuario = null;
            TipoTrilha tipoTrilha = TipoTrilha.Alteracao;

            if (usuarioViewModel.Id == 0)
            {
                usuario = new Usuario();
                usuario.Senha = this.hashString.Do(usuarioViewModel.Senha);
                tipoTrilha = TipoTrilha.Criacao;
            }
            else
            {
                usuario = this.usuarioRepositorio.ObterPorId(usuarioViewModel.Id);
                tipoTrilha = TipoTrilha.Alteracao;
            }

            usuario.Ativo = usuarioViewModel.Ativo;
            usuario.Login = usuarioViewModel.Login;
            usuario.Nome = usuarioViewModel.Nome;
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

            
            this.logarAcaoSistema.Execute(
                 tipoTrilha,
                 "Manutenção de Usuario",
                 "Usuario Criado/Alterado: usuarioLogin: #" + usuarioViewModel.Login);
        }
    }
}