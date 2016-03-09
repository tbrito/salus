namespace Salus.Infra.IoC
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(
            RequestContext requestContext, 
            Type controllerType)
        {
            if ((requestContext == null) || (controllerType == null))
            {
                return null;
            }

            return (Controller)InversionControl.Current.GetInstance(controllerType);
        }
    }
}