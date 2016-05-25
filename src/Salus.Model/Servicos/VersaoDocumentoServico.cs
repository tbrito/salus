namespace Salus.Model.Servicos
{
    using System;
    using Entidades;
    using Repositorios;
    using System.Linq;
    public class VersaoDocumentoServico
    {
        private IDocumentoRepositorio documentoRepositorio;
        private LogarAcaoDoSistema logarAcaoSistema;
        private IVersaoDocumentoRepositorio versaoDocumentoRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;

        public VersaoDocumentoServico(
            IDocumentoRepositorio documentoRepositorio,
            LogarAcaoDoSistema logarAcaoSistema,
            IVersaoDocumentoRepositorio versaoDocumentoRepositorio,
            ISessaoDoUsuario sessaoDoUsuario)
        {
            this.logarAcaoSistema = logarAcaoSistema;
            this.documentoRepositorio = documentoRepositorio;
            this.versaoDocumentoRepositorio = versaoDocumentoRepositorio;
            this.sessaoDoUsuario = sessaoDoUsuario;
        }

        public void Checkout(int id)
        {
            this.documentoRepositorio.Bloquear(id);

            this.logarAcaoSistema.Execute(
                TipoTrilha.Alteracao,
                "Versionamento do documento", 
                "Documento " + id + " bloquado para versionamento");
        }

        public VersaoDocumento Checkin(VersaoDocumento versaoDocumento)
        {
            var versoes = this.versaoDocumentoRepositorio.ObterDoDocumento(versaoDocumento.Documento.Id);
            var numeroVersao = 0;

            if (versoes.Count == 0)
            {
                numeroVersao = 1;
            }
            else
            {
                numeroVersao = versoes
                    .OrderBy(x => x.CriadoEm)
                    .Last()
                    .Versao + 1;
            }

            var versao = new VersaoDocumento
            {
                Comentario = versaoDocumento.Comentario,
                CriadoEm = DateTime.Now,
                CriadoPor = this.sessaoDoUsuario.UsuarioAtual,
                Documento = versaoDocumento.Documento,
                Versao = numeroVersao
            };

            this.versaoDocumentoRepositorio.Salvar(versao);
            this.documentoRepositorio.Desbloquear(versaoDocumento.Documento.Id);

            this.logarAcaoSistema.Execute(
                TipoTrilha.Criacao,
                "Versionamento do documento",
                "Nova versão criada para o documento " + versaoDocumento.Documento.Id);

            return versao;
        }
    }
}