namespace Salus.Infra.Boot
{
    using StructureMap;
    
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
