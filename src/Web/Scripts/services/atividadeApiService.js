angular.module("salus-app").factory("atividadeApi", function ($http) {
    var _getAtividades = function (usuario) {
        return $http.post("api/Atividade/ObterPorUsuario", { UserName:"TiagoBrito"});
    };
    
    return {
        getAtividades: _getAtividades
    };
});