﻿using System;
using System.Collections.Generic;

namespace Salus.Model.Entidades
{
    public class TipoDocumento : Entidade
    {
        public virtual bool Ativo { get; set; }

        public virtual bool EhPasta { get; set; }

        public virtual string Nome { get; set; }

        public virtual TipoDocumento Parent { get; set; }
    }
}