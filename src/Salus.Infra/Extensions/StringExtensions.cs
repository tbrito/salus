using System.Globalization;
using System.Text;

namespace Salus.Infra.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveCaracteresEspeciais(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static int ToInt(this string text)
        {
            int inteiro = 0;

            var conseguiuConverter = int.TryParse(text, out inteiro);

            return inteiro;
        }

        public static bool IsInt(this string text)
        {
            int inteiro = 0;

            return int.TryParse(text, out inteiro);
        }
    }
}
