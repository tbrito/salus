using Salus.Model.Entidades;
using System;

namespace Salus.Model.UI
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Expira { get; set; }
        public DateTime? ExpiraEm { get; set; }
        public Area Area { get; set; }
        public Perfil Perfil { get; set; }
        public string Senha { get; set; }
        public string Login { get; set; }
    }
}