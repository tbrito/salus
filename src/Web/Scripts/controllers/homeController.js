angular.module("salus-app").controller("homeController", function ($scope, atividadeApi, workflowApi) {

    $scope.fluxos = [];
    $scope.atividades = [];

    $scope.obterAtividades = function (usuario) {
        atividadeApi.getAtividades(usuario)
            .success(function (data) {
                $scope.atividades = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao tentar autenticação";
            });
    };

    $scope.obterCaixaEntrada = function () {
        atividadeApi.getCaixaEntrada()
            .success(function (data) {
                $scope.fluxos = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao tentar autenticação";
            });
    };
});