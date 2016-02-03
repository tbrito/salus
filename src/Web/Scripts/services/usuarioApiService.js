angular.module("salus-app").factory("usuarioApi", function ($http, config) {
    var _getUsuario = function (usuario) {
        return $http.get(config.baseUrl + "/usuario", usuario);
    };
    
    return {
        getUsuario: _getUsuario
    };
});