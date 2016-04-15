namespace Salus.Infra.IoC
{
    using System.Collections.Generic;
    using System;
    using StructureMap;
    using StructureMap.Graph;
    using Repositorios;

    public class StructureMapContainer : IInversionControl
    {
        public Container container = new Container();

        public void RegistrarDependencias(string caminhoAssemblies = "")
        {
            this.container.Configure(config => config.Scan(scan =>
            {
                scan.WithDefaultConventions();
                scan.AssembliesFromApplicationBaseDirectory();

                if (string.IsNullOrEmpty(caminhoAssemblies) == false)
                {
                    scan.AssembliesFromPath(caminhoAssemblies);
                }

                scan.LookForRegistries();
                
                scan.AddAllTypesOf<IDatabaseBoot>();
            }));
        }

        public object GetInstance(Type controllerType)
        {
            object instance = this.container.TryGetInstance(controllerType);

            if (instance == null && !controllerType.IsAbstract)
            {
                this.container.Configure(c => c.AddType(controllerType, controllerType));
                instance = this.container.TryGetInstance(controllerType);
            }

            return instance;
        }

        public IEnumerable<T> GetAllInstances<T>()
        {
            return this.container.GetAllInstances<T>();
        }

        public T Resolve<T>()
        {
            return this.container.GetInstance<T>();
        }
    }
}