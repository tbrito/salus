angular.module("salus-app").factory("versaoDocumentoApi", function ($http) {
    var _getPorDocumento = function (documentoId) {
        return $http.get("api/VersaoDocumento/ObterPorDocumento/" + documentoId);
    };

    var _getCheckout = function (documentoId) {
        return $http.get("api/VersaoDocumento/Checkout/" + documentoId);
    };
    
    return {
        getPorDocumento: _getPorDocumento,
        checkout: _getCheckout
    };
});