namespace Salus.Model.Entidades
{
    public class Funcionalidade : Enumeration<Funcionalidade>
    {
        public static readonly Funcionalidade CaixaEntrada = new Funcionalidade(1, "Caixa Entrada");
        public static readonly Funcionalidade Importacao = new Funcionalidade(2, "Importação");
        public static readonly Funcionalidade ImportacaoVariosArquivos = new Funcionalidade(3, "Importação Vários Arquivos");
        public static readonly Funcionalidade PreIndexacao = new Funcionalidade(4, "Pré-Indexação");
        public static readonly Funcionalidade ConfiguracaoUsuario = new Funcionalidade(5, "Configuração de Usuarios");
        public static readonly Funcionalidade ConfiguracaoArea = new Funcionalidade(6, "Configuração de Grupos / Áreas");
        public static readonly Funcionalidade ConfiguracaoPerfil = new Funcionalidade(7, "Configuração de Perfis");
        public static readonly Funcionalidade ConfiguracaoTipoDocumento = new Funcionalidade(8, "Configuração de Tipos de Documentos");
        public static readonly Funcionalidade ConfiguracaoAcessoFuncionalidades = new Funcionalidade(9, "Acesso às Funcionalidades");
        public static readonly Funcionalidade ConfiguracaoAcessoDocumentos = new Funcionalidade(10, "Acesso à documentos");

        private Funcionalidade(int value, string displayName) : base(value, displayName)
        {
        }
    }
}