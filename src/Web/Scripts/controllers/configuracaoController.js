angular.module("salus-app").controller('configuracaoController',
    function ($scope, $location, configuracaoApi, $routeParams) {

    $scope.adicionar = function () {
        $location.path('/Configuracao/Editar/' + 0);
    }

    $scope.carregarParaEdicao = function () {
        var configuracaoId = $routeParams.id;

        if (configuracaoId != 0) {
            configuracaoApi.getConfiguracao(configuracaoId)
                .success(function (data) {
                    $scope.configuracao = data;
                })
                .error(function (data) {
                    $scope.error = "Ops! Algo aconteceu ao adicionar/editar as configuracoes do sistema" + data;
                });
        }
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

    $scope.editarConfiguracao = function (configuracaoId) {
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