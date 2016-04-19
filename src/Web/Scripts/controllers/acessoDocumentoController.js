angular.module("salus-app").controller('acessoDocumentoController', function ($scope, $location, acessoDocumentoApi, perfilApi, usuarioApi, areaApi) {

    $scope.acessoDocumento = {};

    $scope.carregarFormulario = function () {
        var viewModel = {
            PapelId: $scope.acessoDocumento.PapelId,
            AtorId: $scope.acessoDocumento.AtorId
        };

        acessoDocumentoApi.getObterAcesso(viewModel)
            .success(function (data) {
                $scope.acessoDocumento = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os acessos do perfil" + data;
            });
    }

    $scope.carregarAreas = function () {
        areaApi.getAreas()
            .success(function (data) {
                $scope.areas = data;
                $scope.carregarFormulario();
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as áreas" + data;
            });
    }

    $scope.carregarPerfis = function () {
        perfilApi.getPerfis()
            .success(function (data) {
                $scope.perfis = data;
                $scope.carregarFormulario();
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os perfis" + data;
            });
    }
    
    $scope.carregarUsuarios = function () {
        usuarioApi.getUsuarios()
            .success(function (data) {
                $scope.usuarios = data;
                $scope.carregarFormulario();
            })
             .error(function (data) {
                 $scope.error = "Ops! Algo aconteceu ao obter os usuarios" + data;
             });
    }

    $scope.salvar = function (acessoDocumento) {
        acessoDocumentoApi.postSalvarAcesso(acessoDocumento)
            .success(function (data) {
                $scope.sucesso = "Operação realizada com sucesso!";
                $scope.carregarFormulario();
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos" + data;
            });
    }
    
    $scope.popularAcessos = function (posicaoArray, tipoDocumento) {
        $scope.acessoDocumento.TiposDocumentos[posicaoArray] = {
            Id: tipoDocumento.Id,
            Marcado: false
        };
    }

    $scope.inicio = function () {
        $location.path('/AcessoDocumento');
    }
});