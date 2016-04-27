angular.module("salus-app").controller('configuracaoController', function ($scope, $location, configuracaoApi) {

    $scope.configuracoes = [];
    
    $scope.adicionar = function () {
        $location.path('/Configuracao/Editar/' + 0);
    }

    $scope.carregarFormulario = function () {
        configuracaoApi.getConfiguracoes()
            .success(function (data) {
                $scope.configuracoes = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os configuracoes do sistema" + data;
            });
    }

    $scope.editarConfiracao = function (configuracaoId) {
        $location.path('/Configuracao/Editar/' + configuracaoId);
    }
    
    $scope.salvar = function (configuracao) {
        configuracaoApi.salvar(configuracao)
            .success(function (data) {
                $location.path('/Configuracao');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao salvar configuracao " + data;
            });
    }

    $scope.inicio = function () {
        $location.path('/Configuracao');
    }
});