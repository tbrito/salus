angular.module("salus-app").factory("workflowApi", function ($http) {
    var _getCaixaEntrada = function () {
        return $http.get("api/Workflow/CaixaEntrada/0");
    };

    var _getPorDocumento = function (documentoId) {
        return $http.get("api/Workflow/PorDocumento/" + documentoId);
    };
    
    return {
        getCaixaEntrada: _getCaixaEntrada,
        getPorDocumento: _getPorDocumento
    };
});