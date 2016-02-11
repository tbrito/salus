angular.module("salus-app").factory("usuarioApi", function ($http) {
    var _getUsuario = function (usuario) {
        return $http.post("api/Account", usuario);
    };
    
    return {
        getUsuario: _getUsuario
    };
});