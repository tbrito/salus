angular.module("salus-app").controller("pesquisaController", function ($scope, $location) {
    $scope.pesquisar = function (termo) {
        $location.path('/PesquisaView/Resultado/' + termo);
    };

    $scope.abrirDocumento = function (documentoId) {
        $location.path('/View/' + documentoId);
    };
});