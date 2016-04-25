using System;
using System.Collections.Generic;
using Salus.Model.Entidades;
using Salus.Model.Repositorios;

namespace Salus.Model.Servicos
{
    public class AutorizaVisualizacaoDocumento
    {
        private ISessaoDoUsuario sessaoDoUsuario;

        public AutorizaVisualizacaoDocumento(ISessaoDoUsuario sessaoDoUsuario)
        {
            this.sessaoDoUsuario = sessaoDoUsuario;
        }

        public bool Executar(IList<AcessoDocumento> acessos, Documento documento)
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
                if (acesso.TipoDocumento == documento.TipoDocumento)
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
            throw new NotImplementedException();
        }
    }
}