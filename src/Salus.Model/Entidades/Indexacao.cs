namespace Salus.Model.Entidades
{
    public class Indexacao : Entidade
    {
        public virtual string Valor { get; set; }

        public virtual Chave Chave { get; set; }

        public virtual Documento Documento { get; set; }
    }
}