﻿using Salus.Model.Entidades;
using System.Runtime.Serialization;

namespace Salus.Model.UI
{
    [KnownType(typeof(Area))]
    public class AreaViewModel
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Abreviacao { get; set; }
        public bool Segura { get; set; }
        public dynamic Parent { get; set; }
    }
}