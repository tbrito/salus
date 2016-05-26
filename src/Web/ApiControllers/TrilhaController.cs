namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class TrilhaController : ApiController
    {
        private ITrilhaRepositorio trilhaRepositorio;
        
        public TrilhaController()
        {
            this.trilhaRepositorio = InversionControl.Current.Resolve<ITrilhaRepositorio>();
        }

        public IEnumerable<TrilhaViewModel> Get()
        {
            var trilhas = this.trilhaRepositorio.ObterTodosComUsuario();
            var trilhasModel = new List<TrilhaViewModel>();

            foreach (var trilha in trilhas)
            {
                var model = new TrilhaViewModel
                {
                    Data = trilha.Data.ToString("F"),
                    Descricao = trilha.Descricao,
                    Recurso = trilha.Recurso,
                    Tipo = trilha.Tipo.ToString(),
                    Usuario = trilha.Usuario.Nome
                };

                trilhasModel.Add(model);
            }

            return trilhasModel as IEnumerable<TrilhaViewModel>;
        }
        
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}