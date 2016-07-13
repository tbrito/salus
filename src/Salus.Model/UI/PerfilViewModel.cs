namespace Salus.Model.UI
{
    using Salus.Model.Entidades;

    public class PerfilViewModel
    {
        public bool Ativo { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }

        public static PerfilViewModel Criar(Perfil perfil)
        {
            return new PerfilViewModel
            {
                Ativo = perfil.Ativo,
                Id = perfil.Id,
                Nome = perfil.Nome
            };
        }
    }
}