angular.module("salus-app").controller("pesquisaController",
     function ($scope, $location, pesquisaApi, indexacaoApi, $routeParams) {
    
    $scope.pesquisar = function (termo) {
        $location.path('/PesquisaView/Resultado/' + removerCaracterSpecial(termo));
    };

    $scope.procurar = function () {
        var texto = $routeParams.termo;

        if (texto == undefined) {
            return;
        }

        pesquisaApi.pesquisar(texto)
            .success(function (data) {
                $scope.resultadoPesquisa = data;
            });
    };

    $scope.pesquisarPreindexacao = function (documentoId) {
        indexacaoApi.obterSuc(documentoId)
            .success(function (data) {
                pesquisaApi.pesquisar(removerCaracterSpecial(data.Suc))
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