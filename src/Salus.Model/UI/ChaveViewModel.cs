﻿using System.Collections.Generic;

namespace Salus.Model.UI
{
    public class ChaveViewModel
    {
        public int Id { get; set; }
        public IEnumerable<string> ItensLista { get; set; }
        public string Mascara { get; set; }
        public string Nome { get; set; }
        public bool Obrigatorio { get; set; }
        public bool Ativo { get; set; }
        public int TipoDado { get; set; }
        public int TipoDocumentoId { get; set; }
        public string TipoDocumentoNome { get; set; }
        public string ItensListaComoTexto { get; set; }
    }
}
