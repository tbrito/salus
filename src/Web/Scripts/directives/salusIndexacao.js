angular.module('salus-app').directive('salusIndexacao',
    function ($location, preIndexacaoApi, indexacaoApi, chavesApi) {
    return {
        templateUrl: "/UserInterface/Indexacao/_edicao.html",
        restrict: 'AE',
        scope: {
            chaves: '=',
            areas: '=',
            documentocode: '@',
            preindex:'=',
            tipodocumento: '@'
        },
        link: function (scope, element, attrs, ctrl) {
            scope.indexacao = [];

            scope.iniciarIndexacao = function (index, chave) {
                index.CampoId = chave.Id;
                index.DocumentoId = scope.documentocode;
                index.TipoDocumentoId = scope.tipodocumento;
            }

            scope.salvarIndexacao = function (indexacao) {
                if (scope.preindex === false){
                    indexacaoApi.postSalvarIndexacao(indexacao)
                        .success(function (data) {
                            $location.path('/Home');
                        })
                        .error(function (data) {
                            scope.error = "Ops! Algo aconteceu ao salvar documento " + scope.documentocode;
                        });
                } else {
                    preIndexacaoApi.salvar(indexacao)
                        .success(function (data) {
                            $location.path('/Preindexacao');
                        })
                        .error(function (data) {
                            scope.error = "Ops! Algo aconteceu ao salvar documento preindexado " + scope.documentocode;
                        });
                }
            }
        }
    }
});