using Headspring;

namespace Salus.Model.Entidades
{
    public class Papel : Enumeration<Papel>
    {
        public static readonly Papel Pefil = new Papel(1, "Perfil");
        public static readonly Papel Area = new Papel(2, "Área");
        public static readonly Papel Usuario = new Papel(3, "Usuário");

        private Papel(int value, string displayName) : base(value, displayName)
        {
        }
    }
}