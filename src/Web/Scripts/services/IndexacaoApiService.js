angular.module("salus-app").factory("indexacaoApi", function ($http) {
    
    var _postSalvarIndexacao = function (indexacao) {
        return $http.post("api/Indexacao/Salvar/", indexacao);
    };

    var _getPorDocumento = function (documentoId) {
        return $http.get("api/Indexacao/PorDocumento/" + documentoId);
    };
    
    return {
        postSalvarIndexacao: _postSalvarIndexacao,
        getPorDocumento: _getPorDocumento
    };
});