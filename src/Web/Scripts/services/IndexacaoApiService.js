angular.module("salus-app").factory("indexacaoApi", function ($http) {
    
    var _postSalvarIndexacao = function (indexacao) {
        return $http.post("api/Indexacao/Salvar/", indexacao);
    };

    var _postSalvarIndex = function (indexacao) {
        return $http.put("api/Indexacao/SalvarChave/" + indexacao.Id, indexacao);
    };

    var _getPorDocumento = function (documentoId) {
        return $http.get("api/Indexacao/PorDocumento/" + documentoId);
    };
    
    return {
        postSalvarIndexacao: _postSalvarIndexacao,
        postSalvarIndex: _postSalvarIndex,
        getPorDocumento: _getPorDocumento
    };
});