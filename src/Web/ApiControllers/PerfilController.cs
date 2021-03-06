﻿namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class PerfilController : ApiController
    {
        private IPerfilRepositorio perfilRepositorio;
        private IUsuarioRepositorio usuarioRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;
        private HashString hashString;

        public PerfilController()
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
        public void Salvar([FromBody]Perfil perfil)
        {
            Perfil perfilMontado = null;

            if (perfil.Id == 0)
            {
                perfilMontado = new Perfil();
            }
            else
            {
                perfilMontado = this.perfilRepositorio.ObterPorId(perfil.Id);
            }

            perfilMontado.Ativo = perfil.Ativo;
            perfilMontado.Nome = perfil.Nome;

            this.perfilRepositorio.Salvar(perfilMontado);
        }
        
        // PUT api/<controller>/5
        [HttpPut]
        public void Ativar(int id, [FromBody]string value)
        {
            this.perfilRepositorio.Reativar(id);
        }

        [HttpDelete]
        public void Excluir(int id)
        {
            this.perfilRepositorio.MarcarComoInativo(id);
        }
    }
}