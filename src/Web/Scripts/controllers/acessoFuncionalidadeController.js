angular.module("salus-app").controller('acessoFuncionalidadeController', function ($scope, $location, acessoFuncionalidadeApi, perfilApi, usuarioApi) {

    $scope.perfis = [];
    $scope.areas = [];
    $scope.usuarios = [];
    $scope.funcionalidade = {};
    $scope.funcionalidades = {};

    $scope.carregarFormulario = function () {
        $scope.funcionalidade.PapelId = "P";
        $scope.funcionalidade.AtorId = 1;

        acessoFuncionalidadeApi.getObterAcesso($scope.funcionalidade.PapelId, $scope.funcionalidade.AtorId)
            .success(function (data) {
                $scope.funcionalide = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as áreas" + data;
            });

        funcionalidadeApi.getTodos($)
            .success(function (data) {
                $scope.funcionalides = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as áreas" + data;
            });
    }

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