using System;

namespace Salus.Infra.DataAccess
{
    public interface IUnidadeDeTrabalho : IDisposable
    {
        bool EstaAberta { get; }

        IUnidadeDeTrabalho Iniciar();

        void Commit();

        void RollBack();
    }
}