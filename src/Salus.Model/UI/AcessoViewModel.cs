using System.Collections.Generic;

namespace Salus.Model.UI
{
    public class AcessoViewModel
    {
        public AcessoViewModel()
        {
            this.PapelId = 0;
            this.AtorId = 0;
            this.Funcionalidades = new List<FuncionalidadeViewModel>();
        }

        public int PapelId { get; set; }

        public int AtorId { get; set; }

        public IList<FuncionalidadeViewModel> Funcionalidades { get; set;  }
    }
}