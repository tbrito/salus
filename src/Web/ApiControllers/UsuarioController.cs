﻿namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class UsuarioController : ApiController
    {
        private IUsuarioRepositorio usuarioRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;
        private SalvarUsuarioServico salvarUsuarioServico;
        private LogarAcaoDoSistema logarAcaoSistema;

        public UsuarioController()
        {
            this.usuarioRepositorio = InversionControl.Current.Resolve<IUsuarioRepositorio>();
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
            this.salvarUsuarioServico = InversionControl.Current.Resolve<SalvarUsuarioServico>();
            this.logarAcaoSistema = InversionControl.Current.Resolve<LogarAcaoDoSistema>();
        }

        public IEnumerable<UsuarioViewModel> Get()
        {
            var usuarios = this.usuarioRepositorio
                .ObterTodosComAreaEPerfil();

            var usuariosViewModel = new List<UsuarioViewModel>();

            foreach (var usuario in usuarios)
            {
                var viewModel = new UsuarioViewModel
                {
                    Id = usuario.Id,
                    Ativo = usuario.Ativo,
                    Email = usuario.Email,
                    Expira = usuario.Expira,
                    ExpiraEm = usuario.ExpiraEm,
                    Nome = usuario.Nome,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    Area = AreaViewModel.Criar(usuario.Area),
                    Perfil = PerfilViewModel.Criar(usuario.Perfil)
                };
                
                usuariosViewModel.Add(viewModel);
            }
            return usuariosViewModel as IEnumerable<UsuarioViewModel>;
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
            this.salvarUsuarioServico.Execute(usuarioViewModel);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Ativar(int id, [FromBody]string value)
        {
            this.usuarioRepositorio.Reativar(id);

            this.logarAcaoSistema.Execute(
                 TipoTrilha.Alteracao,
                 "Manutenção de Usuario",
                 "Usuario foi ativado: usuarioId: #" + id);
        }

        [HttpDelete]
        public void Excluir(int id)
        {
            this.usuarioRepositorio.MarcarComoInativo(id);

            this.logarAcaoSistema.Execute(
                 TipoTrilha.Alteracao,
                 "Manutenção de Usuario",
                 "Usuario foi inativado: usuarioId: #" + id);
        }
    }
}