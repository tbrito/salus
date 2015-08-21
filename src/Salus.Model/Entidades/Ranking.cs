using System.Collections.Generic;

namespace Salus.Model.Entidades
{
    public class Ranking
    {
        public Ranking()
        {
            Hospitais = new List<Hospital>();
        }

        public IList<Hospital> Hospitais
        {
            get; 
            set;
        }
    }
}