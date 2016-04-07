angular.module("salus-app").factory("acessoFuncionalidadeApi", function ($http) {
    
    var _postSalvarAcesso = function (funcionalidade) {
        return $http.post("api/AcessoFuncionalidade/Salvar/", funcionalidade);
    };

    var _getObterAcesso = function (papelId, atorId) {
        return $http.get("api/AcessoFuncionalidade/" + papelId + "/" + atorId);
    };
    
    return {
        postSalvarAcesso: _postSalvarAcesso
    };
});