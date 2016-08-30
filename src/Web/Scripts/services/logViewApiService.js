angular.module("salus-app").factory("logViewApi", function ($http) {
    var _getLogs = function () {
        return $http.get("api/LogView/Obter");
    };
   
    return {
        obter: _getLogs
    };
});