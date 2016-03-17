using Salus.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace Web.ApiControllers
{
    public class ChaveController : ApiController
    {
        [HttpGet]
        public IEnumerable<Chave> PorTipoDocumento(int id)
        {
            ////var atividades = this.atividadeRepositorio.ObterTodosDoUsuario(usuario.UserName);
            var chaves = new List<Chave>();

            chaves.Add(new Chave { Id = 1, Nome = "Cpf / Cnpj", Obrigatorio = true, TipoDado = TipoDado.Mascara, Mascara = @"/(^\d{3}\.\d{3}\.\d{3}\-\d{2}$)|(^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$)/" });
            chaves.Add(new Chave { Id = 2, Nome = "Assunto", Obrigatorio = true, TipoDado = TipoDado.Texto });
            chaves.Add(new Chave { Id = 3, Nome = "Numero Contrato", Obrigatorio = true, TipoDado = TipoDado.Inteiro });
            chaves.Add(new Chave { Id = 4, Nome = "ValorContrato", Obrigatorio = true, TipoDado = TipoDado.Moeda });
            chaves.Add(new Chave { Id = 5, Nome = "Tipo Contrato", Obrigatorio = true, TipoDado = TipoDado.Lista, Lista = new[] { "valor 01", "valor 02", "valor03" } });

            return chaves as IEnumerable<Chave>;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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