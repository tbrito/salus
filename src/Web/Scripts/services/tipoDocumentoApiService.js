angular.module("salus-app").factory("tipoDocumentoApi", function ($http) {
    var _getTiposDeDocumentos = function () {
        return $http.get("api/TipoDocumento");
    };

    var _getTiposDeDocumentosParaIndexar = function () {
        return $http.get("api/TipoDocumento/ParaIndexar/0");
    };

    var _getTipoDocumento = function (tipodocumentoId) {
        return $http.get("api/TipoDocumento/ObterPorId/" + tipodocumentoId);
    };
    
    var _salvar = function (tipoDocumento) {
        return $http.post("api/TipoDocumento/Salvar/", tipoDocumento);
    };

    var _excluir = function (tipodocumento) {
        return $http.delete("api/TipoDocumento/Excluir/" + tipodocumento.Id);
    };
    
    var _ativar = function (tipodocumento) {
        return $http.put("api/TipoDocumento/Ativar/" + tipodocumento.Id);
    };

    return {
        getTiposDeDocumentos: _getTiposDeDocumentos,
        getTiposDeDocumentosParaIndexar: _getTiposDeDocumentosParaIndexar,
        getTipoDocumento: _getTipoDocumento,
        salvar: _salvar,
        excluir: _excluir,
        ativar: _ativar
    };
});