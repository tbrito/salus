angular.module("salus-app").factory("storageApi", function ($http) {
    var _getDocumento = function (documentoId) {
        return $http.get("api/Arquivos/Documento/" + documentoId);
    };
    
    return {
        getDocumento: _getDocumento
    };
});