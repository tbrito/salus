using System;
using System.Collections.Generic;
using Salus.Model.Entidades;
using Salus.Model.Repositorios;

namespace Salus.Model.Servicos
{
    public class AutorizaVisualizacaoDocumento
    {
        private IAcessoDocumentoRepositorio acessoDocumentoRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;
        private IDocumentoRepositorio documentoRepositorio;

        public AutorizaVisualizacaoDocumento(
            ISessaoDoUsuario sessaoDoUsuario,
            IAcessoDocumentoRepositorio acessoDocumentoRepositorio,
            IDocumentoRepositorio documentoRepositorio)
        {
            this.sessaoDoUsuario = sessaoDoUsuario;
            this.acessoDocumentoRepositorio = acessoDocumentoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public bool PossuiAcesso(IList<AcessoDocumento> acessos, Documento documento)
        {
            ////pode acessar o documento se foi o próprio usuario quem o criou
            if (documento.Usuario == this.sessaoDoUsuario.UsuarioAtual)
            {
                return true;
            }

            ////se o documento não foi tipado não deve ser acesso por outra pessoa além do proprietario
            if (documento.TipoDocumento == null)
            {
                return false;
            }

            foreach (var acesso in acessos)
            {
                if (acesso.TipoDocumento.Id == documento.TipoDocumento.Id)
                {
                    if (this.sessaoDoUsuario.UsuarioAtual.Area.Segura)
                    {
                        if (documento.Usuario.Area == this.sessaoDoUsuario.UsuarioAtual.Area)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public int[] ObterConteudosAutorizados(int[] contentsWithTextId)
        {
            var acessos = this.acessoDocumentoRepositorio.ObterDoUsuario(this.sessaoDoUsuario.UsuarioAtual);

            var documentosIds = new List<int>();

            foreach (var documentoId in contentsWithTextId)
            {
                var documento = this.documentoRepositorio.ObterPorIdComTipoDocumento(documentoId);

                if (this.PossuiAcesso(acessos, documento))
                {
                    documentosIds.Add(documentoId);
                } 
            }

            return documentosIds.ToArray();
        }
    }
}