angular.module("salus-app").factory("pesquisaApi", function ($http) {
    var _pesquisar = function (pesquisa) {
        return $http.post("api/PesquisaEngine", pesquisa);
    };

    return {
        pesquisar: _pesquisar
    };
});