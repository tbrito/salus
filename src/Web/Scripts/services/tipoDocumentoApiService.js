angular.module("salus-app").factory("tipoDocumentoApi", function ($http) {
    var _getTiposDeDocumentos = function () {
        return $http.get("api/TipoDocumento");
    };

    var _getTipoDocumento = function (tipodocumentoId) {
        return $http.get("api/TipoDocumento/ObterPorId/" + tipodocumentoId);
    };
    
    var _salvar = function (tipoDocumento) {
        return $http.post("api/TipoDocumento/Salvar/", tipoDocumento);
    };

    var _excluir = function (tipoDocumentoid) {
        return $http.delete("api/TipoDocumento/Excluir/" + tipoDocumentoid);
    };
    
    return {
        getTiposDeDocumentos: _getTiposDeDocumentos,
        getTipoDocumento: _getTipoDocumento,
        salvar: _salvar,
        excluir: _excluir,
    };
});