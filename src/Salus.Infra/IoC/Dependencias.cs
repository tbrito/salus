namespace Salus.Infra.IoC
{
    public class Dependencias
    {
        public static void Registrar(string caminhoAssemblies = "")
        {
            InversionControl.Current.RegistrarDependencias(caminhoAssemblies);                           
        }
    }
}