using StructureMap;

namespace Salus.Infra.Boot
{
    public class RegistrarDependencias : Registry
    {
        public RegistrarDependencias()
        {
            this.Scan(x =>
            {
                x.AssemblyContainingType<RegistrarDependencias>();
                x.WithDefaultConventions();
            });
        }
    }
}
