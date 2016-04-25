namespace Salus.Infra.Mappings
{
    using FluentNHibernate.Mapping;
    using Model.Entidades;

    public class WorkflowMap : ClassMap<Workflow>
    {
        public WorkflowMap()
        {
            this.Table("workflow");
            this.Id(x => x.Id).Column("id").GeneratedBy.Identity();
            this.Map(x => x.CriadoEm, "criadoem");
            this.Map(x => x.FinalizadoEm, "finalizadoem").Nullable();
            this.Map(x => x.Mensagem, "mensagem");
            this.Map(x => x.Status, "status");
            this.Map(x => x.Lido, "lido");
            this.References(x => x.De, "de");
            this.References(x => x.Para, "para");
            this.References(x => x.Documento, "documento_id");
            
            this.DynamicUpdate();
        }
    }
}