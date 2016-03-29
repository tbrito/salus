using Salus.Infra.IoC;
using Salus.Model.Entidades;
using Salus.Model.Servicos;
using System;
using System.Web.Mvc;

namespace Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RegistraNaTrilhaAttribute : AtributeBase, IAuthorizationFilter
    {
        private LogarAcaoDoSistema logarAcaoDoSistema = InversionControl.Current.Resolve<LogarAcaoDoSistema>();

        public TipoTrilha Tipo { get; set; }
        public string Descricao { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var trilha = new Trilha();

            trilha.Data = DateTime.Now;
            trilha.Tipo = this.Tipo;
            trilha.Usuario = this.sessaoDoUsuario.UsuarioAtual;
            trilha.Recurso = "api/account/post";
            trilha.Descricao = this.Descricao;

            this.logarAcaoDoSistema.Execute(trilha);
        }
    }
}