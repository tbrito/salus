angular.module("salus-app").factory("indexacaoApi", function ($http) {
    var _getTiposDeDocumentos = function () {
        return $http.get("api/Indexacao/TiposDeDocumentos");
    };
    
    var _getChavesDoTipo = function (tipodocumentoId) {
        return $http.get("api/Indexacao/Chaves", tipodocumentoId);
    };

    return {
        getTiposDeDocumentos: _getTiposDeDocumentos,
        getChavesDoTipo: _getChavesDoTipo,
    };
});