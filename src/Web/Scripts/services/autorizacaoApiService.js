angular.module("salus-app").factory("autorizacaoApi", function ($cookieStore) {
    var _putSalvar = function (usuario) {
        return $cookieStore.put("usuario_autenticado", usuario);
    };

    var _getObter = function (usuario) {
        return $cookieStore.get("usuario_autenticado");
    };

    var _remover = function (usuario) {
        return $cookieStore.remove("usuario_autenticado");
    };
    
    return {
        salvar: _putSalvar,
        obter: _getObter,
        remover: _remover,
    };
});