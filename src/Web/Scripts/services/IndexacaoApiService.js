angular.module("salus-app").factory("indexacaoApi", function ($http) {
    
    var _postSalvarIndexacao = function (indexacao) {
        return $http.post("api/Indexacao/Salvar/", indexacao);
    };
    
    return {
        postSalvarIndexacao: _postSalvarIndexacao
    };
});