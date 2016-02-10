using NHibernate.AspNet.Identity.Helpers;
using NHibernate.Mapping.ByCode;
using Salus.Model.Entidades;
using SharpArch.NHibernate;
using System.Linq;

namespace Web.App_Start
{
    public class DataConfig
    {
        public static void Configure(ISessionStorage storage)
        {
            var internalTypes = new[] {
                typeof(Usuario)
            };

            var mapping = MappingHelper.GetIdentityMappings(internalTypes);

            string[] mappings = new string[mapping.Items.Length];

            for (int i = 0; i < mapping.Items.Length; i++)
            {
                mappings[i] = mapping.Items[i].ToString();
            }

            NHibernateSession.Init(storage, mappings);
        }

        private static void DefineBaseClass(ConventionModelMapper mapper, System.Type[] baseEntityToIgnore)
        {
            if (baseEntityToIgnore == null) return;
            mapper.IsEntity((type, declared) =>
                baseEntityToIgnore.Any(x => x.IsAssignableFrom(type)) &&
                !baseEntityToIgnore.Any(x => x == type) &&
                !type.IsInterface);
            mapper.IsRootEntity((type, declared) => baseEntityToIgnore.Any(x => x == type.BaseType));
        }
    }
}