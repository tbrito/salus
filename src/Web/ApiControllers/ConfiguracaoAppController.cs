namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using System.Collections.Generic;
    using System.Web.Http;

    public class ConfiguracaoAppController : ApiController
    {
        private IConfiguracaoRepositorio configuracaoRepositorio;
        private LogarAcaoDoSistema logarAcaoSistema;

        public ConfiguracaoAppController()
        {
            this.configuracaoRepositorio = InversionControl.Current.Resolve<IConfiguracaoRepositorio>();
            this.logarAcaoSistema = InversionControl.Current.Resolve<LogarAcaoDoSistema>();
        }

        public IEnumerable<Configuracao> Get()
        {
            var configuracoes = this.configuracaoRepositorio.ObterTodos();
           
            return configuracoes as IEnumerable<Configuracao>;
        }

        [HttpGet]
        public Configuracao ObterPorId(int id)
        {
            var configuracao = this.configuracaoRepositorio.ObterPorId(id);
            return configuracao;
        }

        [HttpPost]
        public void Salvar([FromBody]Configuracao configuracao)
        {
            Configuracao configuracaoMontada = null;

            if (configuracao.Id == 0)
            {
                configuracaoMontada = new Configuracao();
            }
            else
            {
                configuracaoMontada = this.configuracaoRepositorio.ObterPorId(configuracao.Id);
            }

            configuracaoMontada.Chave = configuracao.Chave;
            configuracaoMontada.Valor = configuracao.Valor;

            this.configuracaoRepositorio.Salvar(configuracaoMontada);

            this.logarAcaoSistema.Execute(
                TipoTrilha.Alteracao,
                "Manutenção de Configuracao",
                "Configuração de sistema foi alterada pelo usuario #" + configuracao.Chave);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}