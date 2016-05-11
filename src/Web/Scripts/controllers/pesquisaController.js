angular.module("salus-app").controller("pesquisaController",
     function ($scope, $location, pesquisaApi, $routeParams) {
    
    $scope.pesquisar = function (termo) {
        $location.path('/PesquisaView/Resultado/' + removerCaracterSpecial(termo));
    };

    $scope.procurar = function () {
        var texto = $routeParams.termo;

        if (texto == undefined) {
            return;
        }

        pesquisaApi.pesquisar(texto)
            .success(function (data) {
                $scope.resultadoPesquisa = data;
            });
    };

    $scope.abrirDocumento = function (documentoId) {
        $location.path('/View/' + documentoId);
    };

    function removerCaracterSpecial(texto){
        var resultado = texto.replaceAll('/', '');
        resultado = resultado.replaceAll('.', '');
        resultado = resultado.replaceAll('-', '');
        resultado = resultado.replaceAll('?', '');

        return resultado;
    }

    String.prototype.replaceAll = function (search, replacement) {
        var target = this;
        return target.split(search).join(replacement);
    };
});