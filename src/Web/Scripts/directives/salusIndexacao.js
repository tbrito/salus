angular.module('salus-app').directive('salusIndexacao', function ($location, indexacaoApi, chavesApi) {
    return {
        templateUrl: "/UserInterface/Indexacao/_edicao.html",
        restrict: 'AE',
        scope: {
            chaves: '=',
            areas: '=',
            documentocode: '@'
        },
        link: function (scope, element, attrs, ctrl) {
            scope.indexacao = [];

            scope.iniciarIndexacao = function (index, chave) {
                index.CampoId = chave.Id;
                index.DocumentoId = scope.documentocode;
            }

            scope.salvarIndexacao = function (indexacao) {
                indexacaoApi.postSalvarIndexacao(indexacao)
                    .success(function (data) {
                        $location.path('/Home');
                    })
                    .error(function (data) {
                        scope.error = "Ops! Algo aconteceu ao salvar documento " + scope.documentocode;
                    });
            }
        }
    }
});