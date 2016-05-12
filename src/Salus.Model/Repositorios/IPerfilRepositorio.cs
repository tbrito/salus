using Salus.Model.Entidades;

namespace Salus.Model.Repositorios
{
    public interface IPerfilRepositorio : IRepositorio<Perfil>
    {
        void MarcarComoInativo(int id);

        void Reativar(int id);
    }
}