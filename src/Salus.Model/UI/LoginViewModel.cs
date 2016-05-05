using System.Collections.Generic;

namespace Salus.Model.UI
{
    public class LoginViewModel
    {
        public string Nome { get; set; }

        public string Senha { get; set; }

        public bool Autenticado { get; set; }

        public IList<FuncionalidadeViewModel> Funcionalidades { get; set; }

        public string Avatar { get; set; }

        public string Login { get; set; }
    }
}
