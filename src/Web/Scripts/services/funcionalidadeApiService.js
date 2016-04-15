angular.module("salus-app").factory("funcionalidadeApi", function ($http) {
    
    var _getObterTodos = function () {
        return $http.get("api/Funcionalidade");
    };
    
    return {
        getObterTodos: _getObterTodos
    };
});