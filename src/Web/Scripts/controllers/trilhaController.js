angular.module("salus-app").controller('trilhaController',
    function ($scope, $location, trilhaApi, $routeParams) {

    $scope.carregarFormulario = function () {
        trilhaApi.obterTrilhas()
            .success(function (data) {
                $scope.trilhas = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as trilhas" + data;
            });
    }
});