angular.module("salus-app").controller("pesquisaController", function ($scope, $location, $routeParams, pesquisaApi) {
    
    $scope.palavra = '';

    $scope.pesquisar = function (termo) {
        $scope.palavra = termo;
        $location.path('/PesquisaView/Resultado/' + termo);
    };

    $scope.procurarTermo = function (texto) {
        console.log($scope.parent.palavra);

        pesquisaApi.pesquisar(texto)
            .success(function (data) {
                $scope.resultadoPesquisa = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao pesquisar" + data;
            });
    };

    $scope.abrirDocumento = function (documentoId) {
        $location.path('/View/' + documentoId);
    };
});