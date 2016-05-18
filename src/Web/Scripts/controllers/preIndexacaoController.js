angular.module("salus-app").controller('preIndexacaoController',
        function ($scope, $location, preIndexacaoApi, $routeParams) {

    $scope.colorBackground = [255, 255, 255];

    var hex = '#03A9F4';
    var rgb = {
        r: 0,
        g: 0,
        b: 0
    };

    $scope.colorBarcode = function () {
        return [rgb.r, rgb.g, rgb.b];
    }

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
        $location.path('/Preindexacao/Imprimir/' + documento.Id);
    }

    $scope.carregarParaEdicao = function () {
        var documentoId = $routeParams.documentoId;
    
        preIndexacaoApi.getPorId(documentoId)
            .success(function (data) {
                $scope.documento = data;
                $scope.barcode = '99988800' + documentoId;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter o documento";
            });
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