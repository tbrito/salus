namespace Salus.Model.Entidades
{
    public class Area : Entidade
    {
        public virtual string Nome { get; set; }

        public virtual string Abreviacao { get; set; }

        public virtual Area Parent { get; set; }

        public bool Ativa { get; set; }

        /// <summary>
        /// Área pode ver apenas os proprios documentos
        /// </summary>
        public bool Segura { get; set; }
    }
}
