namespace Salus.Infra
{
    using ForTests;
    using IoC;
    using Logs;
    using Repositorios;
    using System;
    using System.Reflection;
    using System.Web.Compilation;
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
                var assembly = Assembly.GetEntryAssembly();

                if (assembly == null)
                {
                    try
                    {
                        var webAssembly = BuildManager.GetGlobalAsaxType().BaseType;

                        if (webAssembly != null)
                        {
                            return webAssembly.Assembly.GetName().Name;
                        }
                    }
                    catch (Exception)
                    {
                    }

                    return AppDomain.CurrentDomain.FriendlyName.Replace(" ", "_").Replace(":", "_");
                }

                return assembly.GetName().Name;
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