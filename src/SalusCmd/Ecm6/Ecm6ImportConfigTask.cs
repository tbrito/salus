namespace Veros.Ecm.DataAccess.Tarefas.Ecm6
{
    using Data;
    using Model.Entities;
    using Model.Repository;
    using Veros.Framework;

    public class Ecm6ImportConfigTask : ITarefaM2
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguracaoRepositorio configuracaoRepositorio;

        public Ecm6ImportConfigTask(IUnitOfWork unitOfWork, IConfiguracaoRepositorio configuracaoRepositorio)
        {
            this.unitOfWork = unitOfWork;
            this.configuracaoRepositorio = configuracaoRepositorio;
        }

        public string TextoDeAjuda
        {
            get
            {
                return "Deve ser executado apos a migracao da base do ecm6";
            }
        }

        public string Comando
        {
            get
            {
                return "ecm6 import config";
            }
        }

        public void Executar(params string[] args)
        {
            Log.Application.Info("Salvando configuracoes gerais");

            using (this.unitOfWork.Begin())
            {
                this.configuracaoRepositorio.Salvar(AppSettings.AgenciaKey, "66");
                this.configuracaoRepositorio.Salvar(AppSettings.BicBancoSucKeys, "63;64;65;66");
                this.configuracaoRepositorio.Salvar(AppSettings.BicBancoSucPath, @"C:\Veros\Ecm\Suc");
                this.configuracaoRepositorio.Salvar(AppSettings.SearchIndexPath, @"C:\Veros\Ecm\Index");
                this.configuracaoRepositorio.Salvar(AppSettings.BicBancoPreIndexPath, @"C:\Veros\Ecm\Preindex");
                this.configuracaoRepositorio.Salvar(AppSettings.BicBancoImpDirPath, @"C:\Veros\Ecm\Impdir");
                this.configuracaoRepositorio.Salvar(AppSettings.PurgeNotIndexedDays, "5");
            }
        }
    }
}