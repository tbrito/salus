angular.module("salus-app").controller('viewController', function ($scope, $location, indexacaoApi) {

    $scope.indexacao = [];
    $scope.chaves = [];

    $scope.abrir = function (documentoId) {
        $scope.documentoId = documentoId;


    }
});