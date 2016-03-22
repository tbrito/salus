angular.module("salus-app").controller('tipoDocumentoConfigController', function ($scope, $location, indexacaoApi) {

    $scope.tiposDeDocumento = [];
    $scope.chaves = [];

    $scope.carregarFormulario = function () {
       
        indexacaoApi.getTiposDeDocumentos()
            .success(function (data) {
                $scope.tiposDeDocumento = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

      $scope.editarTipoDocumento = function (tipoDocumentoId) {
       
        tipoDeDocumentoApi.getTipoDeDocumento(tipoDocumentoId)
            .success(function (data) {
                $scope.tiposDeDocumento = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }
});