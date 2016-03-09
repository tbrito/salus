using System;
using System.Collections.Generic;

namespace Salus.Infra.IoC
{
    public interface IInversionControl
    {
        T Resolve<T>();

        void RegistrarDependencias(string caminhoAssemblies);

        object GetInstance(Type controllerType);

        IEnumerable<T> GetAllInstances<T>();
    }
}