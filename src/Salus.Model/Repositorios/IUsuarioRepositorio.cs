using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Usuario Procurar(string userName, string senha);

        Usuario ProcurarPorNome(string nomeUsuario);

        IList<Usuario> ObterTodosComAreaEPerfil();

        Usuario ObterPorIdComParents(int id);

        void MarcarComoInativo(int id);

        void SalvarSenha(int id, string novaSenha);

        void Reativar(int id);
    }
}