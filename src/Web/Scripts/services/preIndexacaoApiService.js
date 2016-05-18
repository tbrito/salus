angular.module("salus-app").factory("preIndexacaoApi", function ($http) {
    var _getDocumentosPendentes = function () {
        return $http.get("api/Preindexacao");
    };

    var _getPorId = function (documentoId) {
        return $http.get("api/Preindexacao/ObterPorId/" + documentoId);
    };

    var _salvar = function (documento) {
        return $http.post("api/Preindexacao/Salvar/", documento);
    };

    var _excluir = function (documento) {
        return $http.delete("api/Preindexacao/Excluir/" + documento.Id);
    };

    return {
        getDocumentosPendentes: _getDocumentosPendentes,
        getPorId: _getPorId,
        salvar: _salvar,
        excluir: _excluir
    };
});