using StructureMap;

namespace Salus.Infra.IoC
{
    public class Registrar<T> : Registry
    {
        public Registrar()
        {
            this.Scan(x =>
            {
                x.WithDefaultConventions();
                x.AssemblyContainingType<T>();
            });
        }
    }
}