angular.module("salus-app").factory("perfilApi", function ($http) {
    var _getPerfis = function () {
        return $http.get("api/Perfil");
    };

    var _getPerfil = function (perfilId) {
        return $http.get("api/Perfil/ObterPorId/" + perfilId);
    };
    
    var _salvar = function (perfil) {
        return $http.post("api/Perfil/Salvar/", perfil);
    };

    var _salvarSenha = function (perfil) {
        return $http.post("api/EdicaoPessoal/SalvarSenha/", perfil);
    };

    var _excluir = function (perfil) {
        return $http.delete("api/Perfil/Excluir/" + perfil.Id);
    };

    var _ativar = function (perfil) {
        return $http.put("api/Perfil/Ativar/" + perfil.Id);
    };
    return {
        getPerfis: _getPerfis,
        getPerfil: _getPerfil,
        salvar: _salvar,
        salvarSenha: _salvarSenha,
        excluir: _excluir,
        ativar: _ativar,
    };
});