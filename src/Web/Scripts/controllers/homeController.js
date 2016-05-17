angular.module("salus-app").controller("homeController", function ($scope, $location, workflowApi) {

    $scope.fluxos = [];
    $scope.atividades = [];

    $scope.carregarFormulario = function (usuario) {
        workflowApi.getCaixaEntrada()
            .success(function (data) {
                $scope.fluxos = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao tentar autenticação";
            });
    };

    $scope.abrirDocumento = function(fluxo) {
        $location.path('/View/' + fluxo.Documento.Id);
    };
});