angular.module("salus-app").factory("versaoDocumentoApi", function ($http) {
    var _getPorDocumento = function (documentoId) {
        return $http.get("api/VersaoDocumento/ObterPorDocumento/" + documentoId);
    };

    var _getCheckout = function (documentoId) {
        return $http.get("api/VersaoDocumento/Checkout/" + documentoId);
    };

    var _getCheckin = function (versao) {
        return $http.post("api/VersaoDocumento/Checkin/", versao);
    };
    
    return {
        getPorDocumento: _getPorDocumento,
        checkout: _getCheckout,
        checkin: _getCheckin,
    };
});