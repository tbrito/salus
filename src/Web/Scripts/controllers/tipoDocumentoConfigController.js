angular.module("salus-app").controller('tipoDocumentoConfigController',
    function ($scope, $location, tipoDocumentoApi, $routeParams) {

    $scope.tiposDeDocumento = [];
    
    $scope.adicionar = function () {
        $location.path('/TipoDocumentoConfig/Editar/' + 0);
    }

    $scope.carregarFormulario = function () {

        tipoDocumentoApi.getTiposDeDocumentos()
            .success(function (data) {
                $scope.tiposDeDocumento = data.filter(function (dado) {
                    return dado.EhPasta == false;
                });
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.editarTipoDocumento = function (tipoDocumentoId) {
        $location.path('/TipoDocumentoConfig/Editar/' + tipoDocumentoId);
    }
    
    $scope.carregarParaEdicao = function () {
        var tipoDocumentoId = $routeParams.tipodocumentoId;

        if (tipoDocumentoId == 0) {
            $scope.tipoDocumento = {};
            $scope.tipoDocumento.EhPasta = false;

            tipoDocumentoApi.getTiposDeDocumentos()
                .success(function (data) {
                    $scope.tipoDocumento.pastas = data.filter(function (dado) {
                        return dado.EhPasta == true;
                    });
                });
        }
        else {
            tipoDocumentoApi.getTipoDocumento(tipoDocumentoId)
                .success(function (data) {
                    $scope.tipoDocumento = data;
                    tipoDocumentoApi.getTiposDeDocumentos()
                        .success(function (data) {
                            $scope.tipoDocumento.pastas = data.filter(function (dado) {
                                return dado.ehPasta == true;
                            });
                        });
                })
                .error(function (data) {
                    $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
                });
        }
    }

    $scope.salvar = function (tipoDocumento) {
        tipoDocumentoApi.salvar(tipoDocumento)
            .success(function (data) {
                $location.path('/ChaveConfig/' + tipoDocumento.id);
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.verChaves= function (tipoDocumento) {
        $location.path('/ChaveConfig/' + tipoDocumento.id);
    }

    $scope.excluir = function (tipodocumentoid) {
        tipoDocumentoApi.excluir(tipoDocumentid)
            .success(function (data) {
                $location.path('/TipoDocumentoConfig');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.inicio = function () {
        $location.path('/TipoDocumentoConfig');
    }
});