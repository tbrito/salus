namespace Veros.Ecm.Model.Entities.Import
{
    /// <summary>
    /// Entidade temporária para migração dos documentos versionados do ecm6
    /// </summary>
    public class Ecm6DocVersionado : Ecm6ToEcm8
    {
        public virtual string FileExtension
        {
            get;
            set;
        }

        public virtual Ecm6ImportStatus ImportStatus
        {
            get;
            set;
        }
    }
}