using Salus.Model.Entidades;
using System.Runtime.Serialization;
using System;

namespace Salus.Model.UI
{
    [KnownType(typeof(TipoDocumento))]
    public class TipoDocumentoViewModel
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public bool EhPasta { get; set; }
        public string Nome { get; set; }
        public int ParentId { get; set; }
        public TipoDocumentoViewModel Parent { get; set; }

        public static TipoDocumentoViewModel Criar(TipoDocumento tipoDocumento)
        {
            var model = new TipoDocumentoViewModel
            {
                Id = tipoDocumento.Id,
                Ativo = tipoDocumento.Ativo,
                EhPasta = tipoDocumento.EhPasta,
                Nome = tipoDocumento.Nome,
            };

            if (tipoDocumento.Parent != null)
            {
                model.ParentId = tipoDocumento.Parent.Id;
                model.Parent = Criar(tipoDocumento.Parent);
            }

            return model;
        }
    }
}
