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

    var _getSuc = function (documentoId) {
        return $http.get("api/Indexacao/ObterSuc/" + documentoId);
    };
    
    return {
        postSalvarIndexacao: _postSalvarIndexacao,
        postSalvarIndex: _postSalvarIndex,
        getPorDocumento: _getPorDocumento,
        obterSuc: _getSuc
    };
});