namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using Veros.Data.Hibernate;
    using Veros.Ecm.Model.Enums;
    using Veros.Framework;

    public class KeyTypesImport
    {
        public Dictionary<int, KeyType> Execute(string message)
        {
            Log.Application.Info(message);

            var items = new Dictionary<int, KeyType>();
            IList<KeyTypeDto> dtos = new List<KeyTypeDto>();

            ImportDatabase.Using(session =>
            {
                dtos = session
                    .CreateSQLQuery("select keytype_code Code, keytype_desc Description from keytype")
                    .SetResultTransformer(CustomResultTransformer<KeyTypeDto>.Do())
                    .List<KeyTypeDto>();
            });

            foreach (var dto in dtos)
            {
                items.Add(dto.Code.ToInt(), this.GetKeyType(dto.Description));
            }

            return items;
        }

        private KeyType GetKeyType(string name)
        {
            //// TODO: verifica se mapeamento está ok
            switch (name.ToLower())
            {
                case "alfanumerico":
                    return KeyType.Text;
                case "numero inteiro":
                    return KeyType.Integer;
                case "numero real":
                    return KeyType.Float;
                case "data":
                    return KeyType.Date;
                case "indicador (s/n)":
                    return KeyType.Boolean;
                case "data limite":
                    return KeyType.Date;
                case "lista de palavras":
                    return KeyType.List;
                case "núm. doc geral":
                    return KeyType.Text;
                case "cnpj":
                    return KeyType.CpfCnpj;
                case "mês-ano":
                    return KeyType.MonthYear;
                default:
                    return KeyType.Text;
            }
        }

        public class KeyTypeDto
        {
            public string Code
            {
                get;
                set;
            }

            public string Description
            {
                get;
                set;
            }
        }
    }
}