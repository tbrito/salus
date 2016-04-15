using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.UI
{
    public class AcessoViewModel
    {
        public int Id { get; set; }

        public int PapelId { get; set; }

        public int AtorId { get; set; }

        public IList<FuncionalidadeViewModel> Funcionalidades { get; set;  }
    }
}