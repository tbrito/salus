namespace Salus.Infra.Repositorios
{
    using SharpArch.NHibernate;
    using Salus.Model.Entidades;

    public class UsuarioRepositorio
    {
        public void SalvarComSenha(Usuario usuario, string password)
        {
            usuario.Senha = password;
            NHibernateSession.Current.SaveOrUpdate(usuario);
        }
    }
}
