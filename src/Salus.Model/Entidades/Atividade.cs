namespace Salus.Model.Entidades
{
    public class Atividade : Entidade
    {
        public virtual int DocumentoId { get; set; }
        public virtual string Acao { get; set; }
        public virtual string Hora { get; set; }
        public virtual string UsuarioNome { get; set; }
    }
}