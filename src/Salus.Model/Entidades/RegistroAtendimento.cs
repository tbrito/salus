using System;

namespace Salus.Model.Entidades
{
    public class RegistroAtendimento : Entidade
    {
        public Usuario Usuario
        {
            get; 
            set;
        }

        public Especialidade Especialidade
        {
            get; 
            set;
        }

        public Hospital Hospital
        {
            get; 
            set;
        }

        public DateTime? EntradaNoHospital
        {
            get; 
            set;
        }

        public DateTime? InicioAtendimentoMedico
        {
            get; 
            set;
        }

        public bool Finalizado
        {
            get; 
            set;
        }
    }
}