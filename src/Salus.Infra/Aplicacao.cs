namespace Salus.Infra
{
    using ForTests;
    using IoC;
    using Logs;
    using Repositorios;
    using System;
    using System.Reflection;

    public class Aplicacao
    {
        public static string Caminho
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static string Nome
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }

        public static int Nucleos
        {
            get
            {
#if DEBUG
                return 1;
#else
                return Environment.ProcessorCount;
#endif
            }
        }

        public static void Boot(string caminhoAssemblies = "")
        {
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

            Log.Initialize();
            Log.App.Info("Aplicacao Iniciada");
        }
    }
}