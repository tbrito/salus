using System;
using Salus.Model.UI;
using Salus.Model.Entidades;
using Salus.Model.Repositorios;

namespace Salus.Model.Servicos
{
    public class DocumentoServico
    {
        private IDocumentoRepositorio documentoRepositorio;

        public DocumentoServico(IDocumentoRepositorio documentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
        }

        public Documento CriaNovo(FileViewModel arquivo)
        {
            var documento = new Documento();

            documento.DataCriacao = arquivo.Created;
            documento.Assunto = arquivo.Subject;
            documento.Tamanho = arquivo.Size;

            this.documentoRepositorio.Salvar(documento);

            return documento;
        }
    }
}