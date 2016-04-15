angular.module("salus-app").factory("acessoFuncionalidadeApi", function ($http) {
    
    var _postSalvarAcesso = function (funcionalidade) {
        return $http.post("api/AcessoFuncionalidade/Salvar/", funcionalidade);
    };

    var _getObterAcesso = function (viewModel) {
        return $http.get("api/AcessoFuncionalidade/ObterPor", viewModel);
    };
    
    return {
        postSalvarAcesso: _postSalvarAcesso,
        getObterAcesso: _getObterAcesso
    };
});