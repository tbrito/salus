namespace Salus.Model.UI
{
    using Salus.Model.Entidades;
    using System;

    public class PerfilViewModel
    {
        public bool Ativo { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }

        public static PerfilViewModel Criar(Perfil perfil)
        {
            var novoPerfil = new PerfilViewModel();

            try
            {
                novoPerfil.Ativo = perfil.Ativo == null ? false : perfil.Ativo;
                novoPerfil.Id = perfil.Id;
                novoPerfil.Nome = string.IsNullOrEmpty(perfil.Nome) ? "sem nome" : perfil.Nome;
            }
            catch (Exception exception)
            {
                return novoPerfil;
            }

            return novoPerfil;
        }
    }
}