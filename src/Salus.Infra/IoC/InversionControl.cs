namespace Salus.Infra.IoC
{
    public class InversionControl
    {
        static InversionControl()
        {
            Current = new StructureMapContainer();
        }

        public static IInversionControl Current
        {
            get; 
            set;
        }
    }
}