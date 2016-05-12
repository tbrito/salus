angular.module("salus-app").factory("grupoDocumentoApi", function ($http) {
    var _getTiposDeDocumentos = function () {
        return $http.get("api/GrupoDocumento");
    };

    var _getTipoDocumento = function (tipodocumentoId) {
        return $http.get("api/GrupoDocumento/ObterPorId/" + tipodocumentoId);
    };
    
    var _salvar = function (tipoDocumento) {
        return $http.post("api/GrupoDocumento/Salvar/", tipoDocumento);
    };

    var _excluir = function (tipoDocumento) {
        return $http.delete("api/GrupoDocumento/Excluir/" + tipoDocumento.Id);
    };

    var _ativar = function (tipoDocumento) {
        return $http.put("api/GrupoDocumento/Ativar/" + tipoDocumento.Id);
    };
    
    return {
        getTiposDeDocumentos: _getTiposDeDocumentos,
        getTipoDocumento: _getTipoDocumento,
        salvar: _salvar,
        excluir: _excluir,
        ativar: _ativar
    };
});