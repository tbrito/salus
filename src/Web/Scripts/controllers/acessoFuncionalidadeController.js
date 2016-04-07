angular.module("salus-app").controller('acessoFuncionalidadeController', function ($scope, $location, acessoFuncionalidadeApi, perfilApi, usuarioApi) {

    $scope.perfis = [];
    $scope.areas = [];
    $scope.usuarios = [];
    
    $scope.carregarAreas = function () {
        areaApi.getAreas()
            .success(function (data) {
                $scope.areas = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as áreas" + data;
            });
    }

    $scope.carregarPerfis = function () {
        perfilApi.getPerfis()
            .success(function (data) {
                $scope.perfis = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os perfis" + data;
            });
    }
    
    $scope.carregarUsuarios = function () {
        usuarioApi.getUsuarios()
            .success(function (data) {
                $scope.usuarios = data;
            })
             .error(function (data) {
                 $scope.error = "Ops! Algo aconteceu ao obter os usuarios" + data;
             });
    }

    $scope.salvar = function (funcionalidade) {
        acessoFuncionalidadeApi.salvar(funcionalidade)
            .success(function (data) {
                $scope.sucesso = "Operação realizada com sucesso!";
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos" + data;
            });
    }
    
    $scope.inicio = function () {
        $location.path('/AcessoFuncionalidade');
    }
});