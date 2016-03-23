angular.module("salus-app").factory("storageApi", function ($http) {
    var _getDocumento = function (documentoId) {
        return $http.get("api/Files/Documento/" + documentoId);
    };
    
    return {
        getDocumento: _getDocumento
    };
});