using System.Web;

namespace Web
{
    public class ContextoWeb
    {
        public static string Caminho
        {
            get
            {
                return HttpRuntime.AppDomainAppPath;
            }
        }
    }
}