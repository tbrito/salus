angular.module("salus-app").controller('tipoDocumentoConfigController', function ($scope, $location, tipoDocumentoApi) {

    $scope.tiposDeDocumento = [];
    
    $scope.carregarFormulario = function () {

        tipoDocumentoApi.getTiposDeDocumentos()
            .success(function (data) {
                $scope.tiposDeDocumento = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.editarTipoDocumento = function (tipoDocumentoId) {
        $location.path('/TipoDocumentoConfig/Editar/' + tipoDocumentoId);
    }

    $scope.carregarParaEdicao = function (tipoDocumentoId) {
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

    $scope.salvar = function (tipodocumento) {
        tipoDocumentoApi.salvar(tipoDocumentoId)
            .success(function (data) {
                $location.path('/TipoDocumentoConfig');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }
});