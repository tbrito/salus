namespace SalusCmd.Ecm6
{
    using System.Threading;

    /// <summary>
    /// TODO: verificar se esta operação pode ir para o veros.framework
    /// </summary>
    public static class ConvertString
    {
        public static string ToPascalCase(string text)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(text.ToLower());
        }
    }
}
