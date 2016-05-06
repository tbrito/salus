angular.module("salus-app").controller("pesquisaController", function ($scope, $location, pesquisaApi) {
    $scope.palavra;

    $scope.pesquisar = function (termo) {
        $scope.palavra = termo;
        $location.path('/PesquisaView/Resultado/' +termo);
    };

    $scope.procurar = function (texto) {
        pesquisaApi.pesquisar($scope.palavra)
            .success(function (data) {
                $scope.resultadoPesquisa = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao pesquisar" + data;
            });
    };

    $scope.abrirDocumento = function (documentoId){
        $location.path('/View/' + documentoId);
    };
});