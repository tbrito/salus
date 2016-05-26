angular.module('salus-app').directive('salusIndexacao', function ($location, indexacaoApi, chavesApi) {
    return {
        templateUrl: "/UserInterface/Indexacao/_edicao.html",
        restrict: 'AE',
        scope: {
            chaves: '='
        },
        link: function (scope, element, attrs, ctrl) {
            
            scope.carregarParametrosIndexacao = function () {
                for (var chave in chaves) {
                    ////indexacao[$index].campoId = chave.Id
                    ////indexacao[$index].documentoId = documentoId
                }
            }

            scope.salvarIndexacao = function () {
                indexacaoApi.postSalvarIndexacao($scope.indexacao)
                    .success(function (data) {
                        $location.path('/Home');
                    })
                    .error(function (data) {
                        $scope.error = "Ops! Algo aconteceu ao salvar documento " + $scope.documentoId;
                    });
            }
        }
    }
});