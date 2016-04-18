angular.module("salus-app").factory("acessoFuncionalidadeApi", function ($http) {
    
    var _postSalvarAcesso = function (acessoFuncionalidade) {
        return $http.post("api/AcessoFuncionalidade/Salvar/", acessoFuncionalidade);
    };

    var _getObterAcesso = function (viewModel) {
        return $http.get("api/AcessoFuncionalidade/ObterPor", viewModel);
    };
    
    return {
        postSalvarAcesso: _postSalvarAcesso,
        getObterAcesso: _getObterAcesso
    };
});