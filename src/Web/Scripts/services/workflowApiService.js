angular.module("salus-app").factory("workflowApi", function ($http) {
    var _getCaixaEntrada = function () {
        return $http.get("api/Workflow/CaixaEntrada/0");
    };
    
    return {
        getCaixaEntrada: _getCaixaEntrada
    };
});