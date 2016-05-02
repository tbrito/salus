using Headspring;

namespace Salus.Model
{
    public class TipoDado : Enumeration<TipoDado>
    {
        public static readonly TipoDado Texto = new TipoDado(0, "Texto");
        public static readonly TipoDado Inteiro = new TipoDado(1, "Inteiro");
        public static readonly TipoDado Real = new TipoDado(2, "Real");
        public static readonly TipoDado Data = new TipoDado(3, "Data");
        public static readonly TipoDado Lista = new TipoDado(8, "Lista");
        public static readonly TipoDado Mascara = new TipoDado(9, "Regex");
        public static readonly TipoDado MesAno = new TipoDado(32, "Mes Ano");
        public static readonly TipoDado CpfCnpj = new TipoDado(17, "Cpf / Cnpj");
        public static readonly TipoDado Area = new TipoDado(18, "Area");
        
        private TipoDado(int value, string displayName) : base(value, displayName)
        {
        }
    }
}