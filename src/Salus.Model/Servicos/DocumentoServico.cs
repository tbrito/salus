using System;
using Salus.Model.UI;
using Salus.Model.Entidades;
using Salus.Model.Repositorios;

namespace Salus.Model.Servicos
{
    public class DocumentoServico
    {
        private IDocumentoRepositorio documentoRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;

        public DocumentoServico(
            IDocumentoRepositorio documentoRepositorio,
            ISessaoDoUsuario sessaoDoUsuario)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.sessaoDoUsuario = sessaoDoUsuario;
        }

        public Documento CriaNovo(FileViewModel arquivo)
        {
            var documento = new Documento();

            documento.DataCriacao = arquivo.Created;
            documento.Assunto = arquivo.Subject;
            documento.Tamanho = arquivo.Size;
            documento.Usuario = this.sessaoDoUsuario.UsuarioAtual;

            this.documentoRepositorio.Salvar(documento);

            return documento;
        }
    }
}