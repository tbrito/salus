using System;

namespace Salus.Model.Entidades
{
    public class RegistroAtendimentoHistorico
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

        public DateTime EntradaNoHospital
        {
            get;
            set;
        }

        public DateTime InicioAtendimentoMedico
        {
            get;
            set;
        }

        public bool Finalizado
        {
            get;
            set;
        }

        public static RegistroAtendimentoHistorico Novo(RegistroAtendimento registroAtendimento)
        {
            return new RegistroAtendimentoHistorico()
            {
                Usuario = registroAtendimento.Usuario,
                EntradaNoHospital = registroAtendimento.EntradaNoHospital.GetValueOrDefault(),
                Especialidade = registroAtendimento.Especialidade,
                Hospital = registroAtendimento.Hospital,
                Finalizado = registroAtendimento.Finalizado,
                InicioAtendimentoMedico = registroAtendimento.InicioAtendimentoMedico.GetValueOrDefault()
            };
        }
    }
}