
using Salus.Infra.IoC;
using Salus.Model.Repositorios;
using System.Web.Mvc;

namespace Web.Filters
{
    public class AtributeBase : FilterAttribute
    {
        protected ISessaoDoUsuario sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
    }
}