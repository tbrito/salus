namespace Salus.Infra.Repositorios
{
    using System;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class PerfilRepositorio : Repositorio<Perfil>, IPerfilRepositorio
    {
        public void MarcarComoInativo(int id)
        {
            this.Sessao
              .CreateQuery("update Perfil set Ativo = false where Id = :id")
              .SetInt32("id", id)
              .ExecuteUpdate();
        }
    }
}