using Salus.Model.Entidades;
using System;
using System.Runtime.Serialization;

namespace Salus.Model.UI
{
    [DataContract]
    [KnownType(typeof(Area))]
    [KnownType(typeof(Perfil))]
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Expira { get; set; }
        public DateTime? ExpiraEm { get; set; }
        public dynamic Area { get; set; }
        public dynamic Perfil { get; set; }
        public string Senha { get; set; }
    }
}