angular.module("salus-app").factory("trilhaApi", function ($http) {
    var _getTrilhas = function () {
        return $http.get("api/Trilha");
    };
    
    return {
        obterTrilhas: _getTrilhas
    };
});