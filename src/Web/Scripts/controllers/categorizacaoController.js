angular.module("salus-app").controller('categorizacaoController', function ($scope, $location, indexacaoApi) {

    $scope.carregarFormulario = function (documentoId) {
        $scope.indexacao.documentoId = documentoId;

        indexacaoApi.getTiposDeDocumentos()
                        .success(function (data) {
                            $scope.tiposDeDocumento = data;
                        })
                        .error(function (data) {
                            $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
                        });
    }

    $scope.selecionarTipo = function () {
        tipoDocumento = $scope.selected;

        indexacaoApi.getChavesDoTipo(tipoDocumento.Id)
                        .success(function (data) {
                            $scope.chaves = data;
                        })
                        .error(function (data) {
                            $scope.error = "Ops! Algo aconteceu ao obter as chaves do tipo de documento" + tipoDocumento.nome;
                        });
    }
});