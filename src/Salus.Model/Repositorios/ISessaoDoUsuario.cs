using Salus.Model.Entidades;

namespace Salus.Model.Repositorios
{
    public interface ISessaoDoUsuario
    {
        Usuario UsuarioAtual { get; }
    }
}