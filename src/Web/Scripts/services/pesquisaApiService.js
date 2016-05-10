angular.module("salus-app").factory("pesquisaApi", function ($http) {
    var _pesquisar = function (pesquisa) {
        var objeto = { Texto: pesquisa };
        return $http.post("api/PesquisaEngine", objeto);
    };

    return {
        pesquisar: _pesquisar
    };
});