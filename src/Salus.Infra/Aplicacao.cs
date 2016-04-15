namespace Salus.Infra
{
    using ForTests;
    using IoC;
    using Repositorios;
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
            
            var bootsToDatabase = InversionControl.Current.GetAllInstances<IDatabaseBoot>();
            var clearsToDatabase = InversionControl.Current.GetAllInstances<IClearDatabase>();

            foreach (var databaseBoot in bootsToDatabase)
            {
                databaseBoot.Execute();
            }

            foreach (var clearDatabase in clearsToDatabase)
            {
                clearDatabase.Execute();
            }
        }
    }
}