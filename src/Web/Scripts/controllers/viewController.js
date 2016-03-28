angular.module("salus-app").controller('viewController', function ($scope, $location, storageApi) {

    $scope.indexacao = [];
    $scope.chaves = [];

    $scope.abrir = function (documentoId) {
        $scope.documentoId = documentoId;

        storageApi.getDocumento(documentoId)
           .success(function (data) {
               $scope.urlDocumento = data.urlDocumento;

               PDFJS.workerSrc = '~/scripts/pdf.worker.js';

               // Fetch the PDF document from the URL using promises.
               PDFJS.getDocument(data.urlDocumento).then(function (pdf) {
                   // Fetch the page.
                   pdf.getPage(1).then(function (page) {
                       var scale = 1.5;
                       var viewport = page.getViewport(scale);

                       // Prepare canvas using PDF page dimensions.
                       var canvas = document.getElementById('the-canvas');
                       var context = canvas.getContext('2d');
                       canvas.height = viewport.height;
                       canvas.width = viewport.width;

                       // Render PDF page into canvas context.
                       var renderContext = {
                           canvasContext: context,
                           viewport: viewport
                       };
                       page.render(renderContext);
                   });
               });
           })
           .error(function (data) {
               $scope.error = "Ops! Algo aconteceu ao tentar baixar a imagem";
           });

    }
});