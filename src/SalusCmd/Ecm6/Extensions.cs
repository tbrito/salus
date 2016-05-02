namespace SalusCmd.Ecm6
{
    using Salus.Infra.Extensions;
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using System.Collections.Generic;

    public static class Extensions
    {
        public static T Resolve<T>(this object value)
        {
            return InversionControl.Current.Resolve<T>();
        }

        public static T Create<T>(this object value, string code) where T : Entidade, new()
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }

            return new T { Id = code.ToInt() };
        }

        public static TEntity GetById<TEntity>(this IDictionary<int, TEntity> dictionary, int id)
        {
            if (dictionary.ContainsKey(id))
            {
                return dictionary[id];
            }

            return default(TEntity);
        }
    }
}
