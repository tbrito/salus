namespace Salus.Infra.Extensions
{
    using System;

    public static class ExceptionExtensions
    {
        public static string GetDetalhesDoErro(this Exception exception)
        {
            if (exception != null)
            {
                return string.Format(
                    "Exception {0} \n Message {1} \n Stack {2} \n InnerException {3}",
                    exception.GetType(),
                    exception.Message,
                    exception.StackTrace,
                    GetDetalhesDoErro(exception.InnerException));
            }

            return "null";
        }
    }
}
