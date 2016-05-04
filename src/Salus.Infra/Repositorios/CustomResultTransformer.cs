namespace Salus.Infra.Repositorios
{
    using System;
    using System.Collections;
    using System.Linq;
    using NHibernate.Transform;

    [Serializable]
    public class CustomResultTransformer<T> : IResultTransformer
    {
        private readonly IResultTransformer transformer;

        public CustomResultTransformer()
        {
            this.transformer = Transformers.AliasToBean<T>();
        }

        public static CustomResultTransformer<T> Do()
        {
            return new CustomResultTransformer<T>();
        }

        public object TransformTuple(object[] tuple, string[] aliases)
        {
            var entityType = typeof(T);
            var newAliases = new string[aliases.Length];

            for (int index = 0; index < aliases.Length; index++)
            {
                var alias = aliases[index];
                var member = entityType.GetMembers().FirstOrDefault(x => x.Name.ToUpper() == alias);
                newAliases[index] = member != null ? member.Name : aliases[index];
            }

            return this.transformer.TransformTuple(
                tuple,
                newAliases);
        }

        public IList TransformList(IList collection)
        {
            return this.transformer.TransformList(collection);
        }
    }
}
