angular.module('salus-app').controller('menuControler', function ($scope, $log, $location, autorizacaoApi, usuarioApi) {
    $scope.items = [
      'The first choice!',
      'And another choice for you.',
      'but wait! A third!'
    ];

    $scope.status = {
        isopen: false
    };

    $scope.toggled = function (open) {
        $log.log('Dropdown is now: ', open);
    };

    $scope.toggleDropdown = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.isopen = !$scope.status.isopen;
    };

    $scope.appendToEl = angular.element(document.querySelector('#dropdown-long-content'));

    $scope.usuario = {};

    $scope.start = function () {
    }

    $scope.abrirUpload = function () {
        $location.path('/Upload');
    };

    $scope.abrirHome = function () {
        $location.path('/Home');
    };

    $scope.abrirTiposDeDocumento = function () {
        $location.path('/TipoDocumentoConfig');
    };

    $scope.abrirGruposDeDocumento = function () {
        $location.path('/GrupoDocumentoConfig');
    };

    $scope.abrirAreas = function () {
        $location.path('/AreaConfig');
    };

    $scope.abrirUsuarios = function () {
        $location.path('/UsuarioConfig');
    };

    $scope.abrirPerfis = function () {
        $location.path('/PerfilConfig');
    };

    $scope.abrirSegurancaDocumentos = function () {
        $location.path('/AcessoDocumento');
    };

    $scope.abrirSegurancaFuncionalidades = function () {
        $location.path('/AcessoFuncionalidade');
    };

    $scope.abrirConfiguracoesDaAplicacao = function () {
        $location.path('/Configuracoes');
    };

    $scope.abrirEdicaoPerfil = function () {
        $location.path('/MeuPerfil/Editar');
    };

    $scope.sair = function () {
        usuarioApi.logout()
            .success(function (data) {
                $location.path('/Login');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao tentar sair do sistema" + data;
            });
    };

    $scope.usuarioTemPermissao = function (funcionalidadeId) {
        $scope.usuario = autorizacaoApi.obter("usuario_autenticado");

        if ($scope.usuario == undefined) {
            return false;
        }

        if ($scope.usuario.Nome = 'admin')
        {
            return true;
        }

        var ok = $scope.usuario.Funcionalidades.filter(function (dado) {
            return dado.Id == funcionalidadeId;
        });

        return ok.length > 0;
    };
});