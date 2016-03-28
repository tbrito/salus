angular.module("salus-app").controller('viewController', function ($scope, $location, storageApi) {

    $scope.indexacao = [];
    $scope.chaves = [];

    $scope.abrir = function (documentoId) {
        $scope.documentoId = documentoId;

        storageApi.getDocumento(documentoId)
           .success(function (data) {
               $scope.urlDocumento = data.urlDocumento;

              PDFViewerApplication
                   .initialize()
                   .then(webViewerInitialized(data.urlDocumento));
           })
           .error(function (data) {
               $scope.error = "Ops! Algo aconteceu ao tentar baixar a imagem";
           });

    }
});