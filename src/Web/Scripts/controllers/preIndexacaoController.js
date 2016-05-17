angular.module("salus-app").controller('preIndexacaoController',
        function ($scope, $location, preIndexacaoApi, $routeParams) {

    $scope.adicionar = function () {
        $location.path('/Preindexacao/Adicionar');
    }

    $scope.carregarFormulario = function () {

        preIndexacaoApi.getDocumentosPendentes()
            .success(function (data) {
                $scope.documentos = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os usuarios";
            });
    }

    $scope.visualizarDocumento = function (documento) {
        $location.path('/View/' + documento.Id);
    }
    
    $scope.imprimirDocumento = function (documento) {
        $location.path('/Imprimir/' + documento.Id);
    }

    $scope.salvar = function (documento) {
        preIndexacaoApi.salvar(documento)
            .success(function (data) {
                $location.path('/Preindexacao');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao salvar pre-indexação";
            });
    }

    $scope.excluir = function (documento) {
        preIndexacaoApi.excluir(documento)
            .success(function (data) {
                $location.path('/Preindexacao');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao excluir preindexacao " + data;
            });
    }
});