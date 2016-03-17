angular.module("salus-app").factory("indexacaoApi", function ($http) {
    var _getTiposDeDocumentos = function () {
        return $http.get("api/TipoDocumento");
    };
    
    var _getChavesDoTipo = function (tipodocumentoId) {
        return $http.get("api/Chave/PorTipoDocumento/" + tipodocumentoId);
    };

    var _postSalvarIndexacao = function (indexacao) {
        return $http.post("api/Indexacao/Salvar/", indexacao);
    };
    
    return {
        getTiposDeDocumentos: _getTiposDeDocumentos,
        getChavesDoTipo: _getChavesDoTipo,
        postSalvarIndexacao: _postSalvarIndexacao
    };
});