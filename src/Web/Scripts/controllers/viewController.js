angular.module("salus-app").controller('viewController', function ($scope, $location, storageApi, $routeParams) {

    $scope.indexacao = [];
    $scope.chaves = [];

    $scope.abrir = function () {
        var documentoId = $routeParams.documentoId;

        storageApi.getDocumento(documentoId)
           .success(function (data) {
               $scope.urlDocumento = data.urlDocumento;
           })
           .error(function (data) {
               $scope.error = "Ops! Algo aconteceu ao tentar baixar a imagem";
           });

    }
});