using System.Collections.Generic;

namespace Salus.Model.UI
{
    public class AcessoDocumentoViewModel
    {
        public AcessoDocumentoViewModel()
        {
            this.PapelId = 0;
            this.AtorId = 0;
            this.TiposDocumentos = new List<TipoPermitidoViewModel>();
        }

        public int PapelId { get; set; }

        public int AtorId { get; set; }

        public IList<TipoPermitidoViewModel> TiposDocumentos { get; set;  }
    }
}