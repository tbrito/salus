angular.module("salus-app").controller('categorizacaoController', 
    function ($scope, $location, indexacaoApi, tipoDocumentoApi, chavesApi, areaApi, $routeParams) {

    $scope.carregarFormulario = function () {
        $scope.documentoId = $routeParams.documentoId;

        tipoDocumentoApi.getTiposDeDocumentosParaIndexar()
            .success(function (data) {
                $scope.tiposDeDocumento = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });

        areaApi.getAreas()
            .success(function (data) {
                $scope.areas = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as areas";
            })
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
});