namespace Salus.Infra
{
    using IoC;
    using System;
    public class Aplicacao
    {
        public static string Caminho
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static void Boot(string caminhoAssemblies = "")
        {
            ////ControllerBuilder.Current
            ////   .SetControllerFactory(new StructureMapControllerFactory());
 
            Dependencias.Registrar(caminhoAssemblies);
        }
    }
}