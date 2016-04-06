angular.module("salus-app").controller("homeController", function ($scope, $location, atividadeApi, workflowApi) {

    $scope.fluxos = [];
    $scope.atividades = [];

    $scope.carregarFormulario = function (usuario) {
        atividadeApi.getAtividades(usuario)
            .success(function (data) {
                $scope.atividades = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao tentar autenticação";
            });

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