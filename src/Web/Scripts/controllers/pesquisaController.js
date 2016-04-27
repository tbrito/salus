angular.module("salus-app").controller("pesquisaController", function ($scope, $location, pesquisaApi) {
    $scope.pesquisar = function (parametro) {
        $location.path('/PesquisaView/Resultado/' + parametro.Texto);
    };

    $scope.procurar = function (parametro) {
        pesquisaApi.pesquisar(parametro)
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