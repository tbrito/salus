angular.module("salus-app").factory("pesquisaApi", function ($http) {
    var _pesquisar = function (pesquisa, pagina) {
        var objeto = { Texto: pesquisa, PaginaId: pagina };
        return $http.post("api/PesquisaEngine", objeto);
    };

    return {
        pesquisar: _pesquisar
    };
});