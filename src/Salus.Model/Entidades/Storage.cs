namespace Salus.Model.Entidades
{
    public class Storage : Entidade
    {
        public virtual string SalusId { get; set; }
        public virtual string FileType { get; set; }
        public virtual string MongoId { get; set; }
        
        public static Storage New(string salusId, string pid, string fileType)
        {
            return new Storage
            {
                SalusId = salusId,
                MongoId = pid,
                FileType = fileType
            };
        }
    }
}