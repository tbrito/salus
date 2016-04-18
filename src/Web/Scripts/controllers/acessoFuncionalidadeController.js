angular.module("salus-app").controller('acessoFuncionalidadeController', function ($scope, $location, acessoFuncionalidadeApi, funcionalidadeApi, perfilApi, usuarioApi, areaApi) {

    $scope.perfis = [];
    $scope.areas = [];
    $scope.usuarios = [];
    $scope.acessosDoPerfil = [];
    $scope.acessoFuncionalidade = { };
    $scope.acessoFuncionalidade.Funcionalidades = [];

    $scope.carregarFormulario = function () {
        
        var viewModel = {
            PapelId: $scope.acessoFuncionalidade.PapelId,
            AtorId: $scope.acessoFuncionalidade.AtorId,
            Id: 1
        };

        acessoFuncionalidadeApi.getObterAcesso(viewModel)
            .success(function (data) {
                $scope.acessosDoPerfil = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os acessos do perfil" + data;
            });

        funcionalidadeApi.getObterTodos()
            .success(function (data) {
                $scope.funcionalidades = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as funcionalidades" + data;
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

    $scope.salvar = function (acessoFuncionalidade) {
        acessoFuncionalidadeApi.postSalvarAcesso(acessoFuncionalidade)
            .success(function (data) {
                $scope.sucesso = "Operação realizada com sucesso!";
                $scope.carregarFormulario();
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos" + data;
            });
    }
    
    $scope.popularAcessos = function (posicaoArray, funcionalidade) {
        $scope.acessoFuncionalidade.Funcionalidades[posicaoArray] = {
            Id: funcionalidade.Value,
            Marcado: false
        };
    }

    $scope.inicio = function () {
        $location.path('/AcessoFuncionalidade');
    }
});