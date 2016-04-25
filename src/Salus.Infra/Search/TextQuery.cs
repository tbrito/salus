namespace Salus.Infra.Search
{
    using Extensions;
    using System;

    public class TextQuery
    {
        public TextQuery(string text)
        {
            this.OriginalText = text;

            if (string.IsNullOrEmpty(text))
            {
                this.Incremented = text;
                return;
            }

            var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 1)
            {
                this.Incremented = string.Format(@"""{0}""", text);
            }
            else
            {
                var startWithWildcard = text.StartsWith("*");
                text = text.RemoveCaracteresEspeciais();

                this.Incremented = string.Format(@"{0}*", text);

                if (startWithWildcard)
                {
                    this.Incremented = "*" + this.Incremented;
                }
            }
        }

        public string Incremented
        {
            get;
            private set;
        }

        public string OriginalText
        {
            get;
            private set;
        }
    }
}