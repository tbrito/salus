namespace Salus.Model.Entidades
{
    public class Storage : Entidade
    {
        public virtual Documento Documento { get; set; }
        public virtual short MongoId { get; set; }
        
        public static Storage New(Documento documento, short pid)
        {
            return new Storage
            {
                Documento = documento,
                MongoId = pid
            };
        }
    }
}