namespace Salus.Model.Entidades
{
    public class Atividade : Entidade
    {
        public int DocumentoId;
        public string Acao { get; set; }
        public string Hora { get; set; }
        public string UsuarioNome { get; set; }
    }
}