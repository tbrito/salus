namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class StorageMap : ClassMap<Storage>
    {
        public StorageMap()
        {
            this.Table("gridstorage");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.MongoId, "mongoid");
            this.Map(x => x.FileType, "filetype");
            this.Map(x => x.SalusId, "salusid");
            
            this.DynamicUpdate();
        }
    }
}