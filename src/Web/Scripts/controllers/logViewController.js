angular.module("salus-app").controller("logViewController", function ($scope, logViewApi) {
    
    $scope.abrir = function () {
        $scope.log = logViewApi.obter();
    }
});