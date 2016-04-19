using System.Collections.Generic;

namespace Salus.Model.UI
{
    public class LoginViewModel
    {
        public string UserName { get; set; }

        public string Senha { get; set; }

        public bool Autenticado { get; set; }

        public IList<FuncionalidadeViewModel> Funcionalidades { get; set; }
    }
}
