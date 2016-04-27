angular.module("salus-app").factory("usuarioApi", function ($http) {
    var _getUserLogin = function (usuario) {
        return $http.post("api/Account", usuario);
    };

    var _logout = function () {
        return $http.get("api/Account/Logout/0");
    };
    
    var _getUsuarios = function () {
        return $http.get("api/Usuario");
    };

    var _getUsuario = function (usuarioid) {
        return $http.get("api/Usuario/ObterPorId/" + usuarioid);
    };

    var _salvar = function (usuario) {
        return $http.post("api/Usuario/Salvar/", usuario);
    };

    var _excluir = function (usuarioid) {
        return $http.delete("api/Usuario/Excluir/" + usuarioid);
    };

    return {
        getUserLogin: _getUserLogin,
        getUsuarios: _getUsuarios,
        getUsuario: _getUsuario,
        salvar: _salvar,
        logout: _logout,
        excluir: _excluir,
    };
});