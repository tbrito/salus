angular.module("salus-app").factory("chavesApi", function ($http) {
    var _getChavesDoTipo = function (tipodocumentoId) {
        return $http.get("api/Chave/PorTipoDocumento/" + tipodocumentoId);
    };
    
    var _getChave = function (chaveid) {
        return $http.get("api/Chave/ObterPorId/" + chaveid);
    };

    var _salvar = function (chave) {
        return $http.post("api/Chave/Salvar/", chave);
    };

    var _excluir = function (chaveid) {
        return $http.delete("api/Chave/Excluir/" + chaveid);
    };

    return {
        getChavesDoTipo: _getChavesDoTipo,
        getChave: _getChave,
        salvar: _salvar,
        excluir: _excluir,
    };
});