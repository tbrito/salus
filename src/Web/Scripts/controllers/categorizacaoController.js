angular.module("salus-app").controller('categorizacaoController', function ($scope, $location, indexacaoApi, tipoDocumentoApi, chavesApi) {

    $scope.indexacao = [];
    $scope.chaves = [];

    $scope.carregarFormulario = function (documentoId) {
        $scope.documentoId = documentoId;

        tipoDocumentoApi.getTiposDeDocumentos()
            .success(function (data) {
                $scope.tiposDeDocumento = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.selecionarTipo = function () {
        tipoDocumentoSelecionadoId = $scope.tipoDocumentoId;

        chavesApi.getChavesDoTipo(tipoDocumentoSelecionadoId)
            .success(function (data) {
                $scope.chaves = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as chaves do tipo de documento" + $scope.tipoDocumentoId;
            });
    }

    $scope.salvarIndexacao = function () {
        indexacaoApi.postSalvarIndexacao($scope.indexacao)
            .success(function (data) {
                $location.path('/Home');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao salvar documento " + $scope.documentoId;
            });
    }
});