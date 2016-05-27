using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Salus.Model.Entidades
{
    public class Area : Entidade
    {
        public virtual string Nome { get; set; }

        public virtual string Abreviacao { get; set; }

        public virtual Area Parent { get; set; }

        public virtual bool Ativo { get; set; }

        /// <summary>
        /// Área pode ver apenas os proprios documentos
        /// </summary>
        public virtual bool Segura { get; set; }

        public virtual IList<Area> SubAreas { get; set; }
    }
}
