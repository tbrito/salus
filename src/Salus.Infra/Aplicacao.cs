namespace Salus.Infra
{
    using IoC;    

    public class Aplicacao
    {
        public static void Boot(string caminhoAssemblies = "")
        {
            ////ControllerBuilder.Current
            ////   .SetControllerFactory(new StructureMapControllerFactory());
 
            Dependencias.Registrar(caminhoAssemblies);
        }
    }
}