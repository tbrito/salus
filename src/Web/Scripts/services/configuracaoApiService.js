angular.module("salus-app").factory("configuracaoApi", function ($http) {
    var _getConfiguracoes = function () {
        return $http.get("api/ConfiguracaoApp");
    };

    var _getConfiguracao = function (configuracaoId) {
        return $http.get("api/ConfiguracaoApp/ObterPorId/" + configuracaoId);
    };
    
    var _salvar = function (configuracao) {
        return $http.post("api/ConfiguracaoApp/Salvar/", configuracao);
    };

    return {
        getConfiguracoes: _getConfiguracoes,
        getConfiguracao: _getConfiguracao,
        salvar: _salvar
    };
});