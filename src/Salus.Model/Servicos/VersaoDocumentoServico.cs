namespace Salus.Model.Servicos
{
    using System;
    using Entidades;
    using Repositorios;

    public class VersaoDocumentoServico
    {
        private IDocumentoRepositorio documentoRepositorio;
        private LogarAcaoDoSistema logarAcaoSistema;

        public VersaoDocumentoServico(
            IDocumentoRepositorio documentoRepositorio,
            LogarAcaoDoSistema logarAcaoSistema)
        {
            this.logarAcaoSistema = logarAcaoSistema;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Checkout(int id)
        {
            this.documentoRepositorio.Bloquear(id);

            this.logarAcaoSistema.Execute(
                TipoTrilha.Alteracao, 
                "Documento " + id + " bloquado para versionamento");
        }
    }
}