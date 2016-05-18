angular.module("salus-app").controller('viewController', 
    function ($scope, $location, storageApi, $routeParams, indexacaoApi, versaoDocumentoApi, workflowApi) {

    $scope.indexacao = [];
    $scope.chaves = [];

    $scope.abrir = function () {
        $scope.documentoId = $routeParams.documentoId;

        storageApi.getDocumento($scope.documentoId)
           .success(function (data) {
               $scope.urlDocumento = data.urlDocumento;
           })
           .error(function (data) {
               $scope.error = "Imagem nao pode ser encontrada!";
           });

        indexacaoApi.getPorDocumento($scope.documentoId)
            .success(function (data) {
               $scope.indexacao = data;
           })
           .error(function (data) {
               $scope.error = "Ops! Algo aconteceu ao obter indexacao do documento";
           });

        versaoDocumentoApi.getPorDocumento($scope.documentoId)
            .success(function (data) {
                $scope.versoes = data
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as versões do documento";
            });

        workflowApi.getPorDocumento($scope.documentoId)
            .success(function (data) {
                $scope.workflow = data
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as versões do documento";
            });
    }
});