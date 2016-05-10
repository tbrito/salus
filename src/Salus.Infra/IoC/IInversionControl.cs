using System;
using System.Collections.Generic;

namespace Salus.Infra.IoC
{
    public interface IInversionControl
    {
        T Resolve<T>();

        object Resolve(Type type);

        void RegistrarDependencias(string caminhoAssemblies);

        object GetInstance(Type controllerType);

        IEnumerable<T> GetAllInstances<T>();
    }
}