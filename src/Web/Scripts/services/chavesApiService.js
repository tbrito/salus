angular.module("salus-app").factory("chavesApi", function ($http) {
    var _getChavesDoTipo = function (tipodocumentoId) {
        return $http.get("api/Chave/PorTipoDocumento/" + tipodocumentoId);
    };
    
    return {
        getChavesDoTipo: _getChavesDoTipo,
    };
});