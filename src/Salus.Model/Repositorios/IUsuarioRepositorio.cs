using Salus.Model.Entidades;

namespace Salus.Model.Repositorios
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Usuario Procurar(string userName, string senha);

        Usuario ProcurarPorNome(string nomeUsuario)
    }
}