namespace Salus.Model.Servicos
{
    using Salus.Model.Entidades;
    using Salus.Model.UI;
    using System.Collections.Generic;

    public class SalvarConteudoServico
    {
        private DocumentoServico documentoServico;
        private StorageServico storageServico;

        public SalvarConteudoServico(
            DocumentoServico documentoServico, 
            StorageServico storageServico)
        {
            this.documentoServico = documentoServico;
            this.storageServico = storageServico;
        }

        public IList<Documento> Executar(IList<FileViewModel> arquivos)
        {
            var documentos = new List<Documento>();

            foreach (var arquivo in arquivos)
            {
                var documento = this.documentoServico.CriaNovo(arquivo);
                this.storageServico.Adicionar(arquivo.Path, documento);

                documentos.Add(documento);
            }

            return documentos;
        }
    }
}