angular.module("salus-app").factory("areaApi", function ($http) {
    var _getAreas = function () {
        return $http.get("api/Area");
    };

    var _getArea = function (areaid) {
        return $http.get("api/Area/ObterPorId/" + areaid);
    };
    
    var _salvar = function (area) {
        return $http.post("api/Area/Salvar/", area);
    };

    var _excluir = function (areaid) {
        return $http.delete("api/Area/Excluir/" + areaid);
    };
    
    return {
        getAreas: _getAreas,
        getArea: _getArea,
        salvar: _salvar,
        excluir: _excluir,
    };
});