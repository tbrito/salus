angular.module("salus-app").factory("versaoDocumentoApi", function ($http) {
    var _getPorDocumento = function (documentoId) {
        return $http.get("api/VersaoDocumento/ObterPorDocumento/" + documentoId);
    };

    return {
        getPorDocumento: _getPorDocumento,
       
    };
});