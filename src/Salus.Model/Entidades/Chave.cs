﻿using System;

namespace Salus.Model.Entidades
{
    public class Chave : Entidade
    {
        public virtual string[] Lista
        {
            get
            {
                if (string.IsNullOrEmpty(this.ItensLista) == false)
                {
                    return this.ItensLista.Split(new[] { Environment.NewLine, "\n" } , StringSplitOptions.RemoveEmptyEntries);
                }

                return new string[] { "Sem Itens" };
            }
        }

        public virtual string ItensLista { get; set;  }

        public virtual string Mascara { get; set; }

        public virtual string Nome { get; set; }

        public virtual bool Obrigatorio { get; set; }

        public virtual bool Ativo { get; set; }

        public virtual TipoDado TipoDado { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }
    }
}