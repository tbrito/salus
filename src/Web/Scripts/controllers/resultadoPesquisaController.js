angular.module("salus-app").controller("resultadoPesquisaController", function ($scope, $location, pesquisaApi) {

  $scope.procurarTermo = function (texto) {
        pesquisaApi.pesquisar(texto)
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