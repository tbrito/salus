angular.module("salus-app").controller("pesquisaController",
     function ($scope, $location, pesquisaApi, indexacaoApi, $routeParams) {
    
    $scope.pesquisar = function (termo, pagina) {
        $location.path('/PesquisaView/Resultado/' + removerCaracterSpecial(termo) + '/' + pagina);
    };

    $scope.procurar = function () {
        var texto = $routeParams.termo;
        var pagina = $routeParams.pagina;
        
        if (texto == undefined) {
            return;
        }

        $scope.palavra = texto;

        pesquisaApi.pesquisar(texto, pagina)
            .success(function (data) {
                $scope.resultadoPesquisa = data;
            });
    };

    $scope.pesquisarPreindexacao = function (documentoId) {
        indexacaoApi.obterSuc(documentoId)
            .success(function (data) {
                pesquisaApi.pesquisar(removerCaracterSpecial(data.Suc), 1)
                    .success(function (data) {
                        $scope.resultadoPesquisa = data;
                });
            })
            .error(function (data) {
                $scope.error = "A pesquisa não conseguiu trazer o que você pediu";
            })
    }

    $scope.abrirDocumento = function (documentoId) {
        $location.path('/View/' + documentoId);
    };

    function removerCaracterSpecial(texto){
        var resultado = texto.replaceAll('/', '');
        resultado = resultado.replaceAll('.', '');
        resultado = resultado.replaceAll('-', '');
        resultado = resultado.replaceAll('?', '');

        return resultado;
    }

    String.prototype.replaceAll = function (search, replacement) {
        var target = this;
        return target.split(search).join(replacement);
    };
});