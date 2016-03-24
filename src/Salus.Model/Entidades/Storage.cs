namespace Salus.Model.Entidades
{
    public class Storage : Entidade
    {
        public virtual Documento Documento { get; set; }
        public virtual string FileType { get; set; }
        public virtual string MongoId { get; set; }
        
        public static Storage New(Documento documento, string pid, string fileType)
        {
            return new Storage
            {
                Documento = documento,
                MongoId = pid,
                FileType = fileType
            };
        }
    }
}