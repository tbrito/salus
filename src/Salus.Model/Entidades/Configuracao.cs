namespace Salus.Model.Entidades
{
    public class Configuracao : Entidade
    {
        public virtual string Chave { get; set; }

        public virtual string Valor { get; set; }
    }
}