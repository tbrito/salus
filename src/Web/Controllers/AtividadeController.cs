namespace Web.Controllers
{
    using Salus.Infra.Repositorios;
    using Salus.Model.Entidades;
    using Salus.Model.UI;
    using System.Collections.Generic;
    using System.Web.Http;

    public class AtividadeController : ApiController
    {
        private AtividadeRepositorio atividadeRepositorio;

        public AtividadeController() : this(new AtividadeRepositorio())
        {
        }

        public AtividadeController(AtividadeRepositorio usuarioRepositorio)
        {
            this.atividadeRepositorio = usuarioRepositorio;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IEnumerable<Atividade> PostObterPorUsuario([FromBody]LoginViewModel usuario)
        {
            ////var atividades = this.atividadeRepositorio.ObterTodosDoUsuario(usuario.UserName);
            var atividades = new List<Atividade>();

            atividades.Add(new Atividade { UsuarioNome = "Tiago Brito", Acao = "Publicou", DocumentoId = 12435, Hora = "Agora Pouco" });
            atividades.Add(new Atividade { UsuarioNome = "Roberto Menescal", Acao = "Compartilhou com você", DocumentoId = 54657, Hora = "Há 3 dias atras" });

            return atividades as IEnumerable<Atividade>;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}