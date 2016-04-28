namespace Salus.Model.Servicos
{
    using Salus.Model.Entidades;
    using Salus.Model.UI;
    using System.Collections.Generic;

    public class SalvarConteudoServico
    {
        private DocumentoServico documentoServico;
        private StorageServico storageServico;
        private WorkflowServico workflowServico;
        
        public SalvarConteudoServico(
            DocumentoServico documentoServico, 
            StorageServico storageServico,
            WorkflowServico workflowServico)
        {
            this.documentoServico = documentoServico;
            this.storageServico = storageServico;
            this.workflowServico = workflowServico;
        }

        public IList<Documento> Executar(IList<FileViewModel> arquivos)
        {
            var documentos = new List<Documento>();

            foreach (var arquivo in arquivos)
            {
                var documento = this.documentoServico.CriaNovo(arquivo);
                this.storageServico.Adicionar(arquivo.Path, documento.Id.ToString());
                this.workflowServico.Iniciar(documento);

                documentos.Add(documento);
            }

            return documentos;
        }

        public void SalvarFoto(List<FileViewModel> arquivos, string usuarioId)
        {
            foreach (var arquivo in arquivos)
            {
                this.storageServico.Adicionar(
                    arquivo.Path,
                    usuarioId);
            }
        }
    }
}