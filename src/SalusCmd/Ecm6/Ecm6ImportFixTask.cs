namespace Veros.Ecm.DataAccess.Tarefas.Ecm6
{
    using System.Linq;
    using SystemKeys;
    using Dapper;
    using Model.Enums;
    using Veros.Framework;

    public class Ecm6ImportFixTask : ITarefaM2
    {
        public string TextoDeAjuda
        {
            get
            {
                return "Remove registros fantasmas (referencias à ids inexistentes)";
            }
        }

        public string Comando
        {
            get
            {
                return "ecm6 import fix";
            }
        }

        public void Executar(params string[] args)
        {
            this.FixProfileCategories();
            this.FixSystemKeys();
            this.FixCustomerDossierValue();
            this.FixFileVersion();
        }

        private void FixFileVersion()
        {
            Log.Application.Info("Fixando contents.version ...");

            DapperHelper.UsingConnection(conn =>
            {
                var ret = conn.Execute(
                    "update contents set version=1 where type=1 and (version=0 or version is null)", 
                    commandTimeout: 0);

                Log.Application.InfoFormat("{0} contents corrigidos", ret);
            });

            Log.Application.Info("contents.version ok");
        }

        private void FixSystemKeys()
        {
            Log.Application.Info("Fixando system keys...");

            DapperHelper.UsingConnection(
                conn => conn.Execute("update system_keys set type=22 where name='Matricula'"));

            Log.Application.Info("Fixando system keys OK");
        }

        private void FixCustomerDossierValue()
        {
            Log.Application.Info("Populando coluna contents.value");

            DapperHelper.UsingConnection(conn =>
            {
                const string Sql = @"
select 
	c.id ContentId,
	i.value IndexValue
from indexes i
inner join contents c on (c.id = i.content_id)
inner join keys k on (k.id = i.key_id)
inner join system_keys s on (s.id = k.system_key_id)
where 
    (s.type=@TypeCnpj or s.type=@TypeMatricula) and 
    (c.type=@CustomerDossier or c.type=@EmployeeDossier) and 
    c.value is null";

                var dossiers = conn.Query(Sql, new
                {
                    TypeCnpj = KeyType.CpfCnpj.ToInt(),
                    TypeMatricula = KeyType.Matricula.ToInt(),
                    CustomerDossier = ContentType.CustomerDossier.ToInt(),
                    EmployeeDossier = ContentType.EmployeeDossier.ToInt()
                });

                foreach (var dossier in dossiers)
                {
                    int contentId = dossier.ContentId;
                    string indexValue = dossier.IndexValue;

                    Log.Application.InfoFormat("Content #{0} atribuindo value #{1}", contentId, indexValue);

                    conn.Execute("update contents set value=@Value where id=@Id", new
                    {
                        Value = indexValue,
                        Id = contentId
                    });
                }              

                Log.Application.Info("contents.value Ok");
            });
        }

        private void FixProfileCategories()
        {
            Log.Application.Info("Removendo profiles apontando para categories inexistentes");

            DapperHelper.UsingConnection(conn =>
            {
                const string Sql = @"
select 
    category_id 
from 
    profile_categories 
where 
    category_id not in (select id from categories)
group by 
    category_id";

                var categoryIds = conn.Query<int>(Sql);

                Log.Application.InfoFormat("Encontrado {0} categories. Excluindo: {1}", categoryIds.Count(), categoryIds.Join(","));

                conn.Execute("delete from profile_categories where category_id in @CategoryIds", new
                {
                    CategoryIds = categoryIds
                });

                Log.Application.Info("profile_categories Ok");
            });
        }
    }
}