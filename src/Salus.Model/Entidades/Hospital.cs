﻿using System.Collections.Generic;

namespace Salus.Model.Entidades
{
    public class Hospital : Entidade
    {
        public int TempoAtendimento
        {
            get; 
            set;
        }

        public string Nome
        {
            get; 
            set;
        }
    }
}
