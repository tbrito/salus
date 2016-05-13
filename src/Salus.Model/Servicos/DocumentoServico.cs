using System;
using Salus.Model.UI;
using Salus.Model.Entidades;
using Salus.Model.Repositorios;

namespace Salus.Model.Servicos
{
    public class DocumentoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ISessaoDoUsuario sessaoDoUsuario;

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

            documento.DataCriacao = DateTime.Now;
            documento.Assunto = "(Sem Assunto)";
            documento.Tamanho = 0;
            documento.Usuario = this.sessaoDoUsuario.UsuarioAtual;

            this.documentoRepositorio.Salvar(documento);

            return documento;
        }
    }
}