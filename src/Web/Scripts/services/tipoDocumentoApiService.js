angular.module("salus-app").factory("tipoDocumentoApi", function ($http) {
    var _getTiposDeDocumentos = function () {
        return $http.get("api/TipoDocumento");
    };

    var _getTipoDocumento = function (tipodocumentoId) {
        return $http.get("api/TipoDocumento/" + tipodocumentoId);
    };
    
    var _salvar = function (tipoDocumento) {
        return $http.post("api/TipoDocumentoConfig/Salvar/", tipoDocumento);
    };
    
    return {
        getTiposDeDocumentos: _getTiposDeDocumentos,
        getTipoDocumento: _getTipoDocumento,
        salvar: _salvar
    };
});