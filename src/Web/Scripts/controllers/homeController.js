angular.module("salus-app").controller("homeController", function ($scope, atividadeApi) {

    $scope.obterAtividades = function (usuario) {
        atividadeApi.getAtividades(usuario)
            .success(function (data) {
                $scope.atividades = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao tentar autenticação";
            });
    };
});