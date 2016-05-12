angular.module("salus-app").controller('grupoDocumentoConfigController',
    function ($scope, $location, grupoDocumentoApi, $routeParams) {

    $scope.gruposDocumentos = [];
    
    $scope.adicionar = function () {
        $location.path('/GrupoDocumentoConfig/Editar/' + 0);
    }

    $scope.carregarFormulario = function () {

        grupoDocumentoApi.getTiposDeDocumentos()
            .success(function (data) {
                $scope.gruposDocumentos = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.editarGrupoDocumento = function (grupoDocumentoId) {
        $location.path('/GrupoDocumentoConfig/Editar/' + grupoDocumentoId);
    }
    
    $scope.carregarParaEdicao = function () {
        var grupoDocumentoId = $routeParams.grupodocumentoId;

        if (grupoDocumentoId != 0) {
            grupoDocumentoApi.getTipoDocumento(grupoDocumentoId)
                .success(function (data) {
                    $scope.grupoDocumento = data;
                })
                .error(function (data) {
                    $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
                });
        }
    }

    $scope.salvar = function (grupoDocumento) {
        grupoDocumentoApi.salvar(grupoDocumento)
            .success(function (data) {
                $location.path('/GrupoDocumentoConfig');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.excluir = function (grupodocumento) {
        grupoDocumentoApi.excluir(grupodocumento)
            .success(function (data) {
                grupodocumento.Ativo = false;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.ativar = function (grupodocumento) {
        grupoDocumentoApi.ativar(grupodocumento)
            .success(function (data) {
                grupodocumento.Ativo = true;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.inicio = function () {
        $location.path('/GrupoDocumentoConfig');
    }
});