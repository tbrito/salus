angular.module("salus-app", [
                    "ngMessages", 
                    "ngRoute", 
                    "ui.bootstrap",
                    "ngFileUpload",
                    "ngCookies"])
    .config(config);
    
function config($routeProvider, $locationProvider) {
    var viewBase = 'views/';
    $routeProvider
        .when('/Home', {
            controller: 'homeController',
            templateUrl: 'Home/Me',
        })
        .when('/Login', {
            controller: 'loginController',
            templateUrl: 'Login/Index'
        })
        .when('/Upload', {
            controller: 'uploadController',
            templateUrl: 'Documento/Novo'
        })
        .when('/Categorizar/:documentoId', {
            controller: 'categorizacaoController',
            templateUrl: function (params) { return 'Categorizar/' + params.documentoId; }
        })
        .when('/UsuarioConfig', {
            controller: 'usuarioConfigController',
            templateUrl: 'UsuarioConfig/Index'
        })
        .when('/UsuarioConfig/Editar/:usuarioId', {
            controller: 'usuarioConfigController',
            templateUrl: function (params) { return 'UsuarioConfig/Editar/' + params.usuarioId; }
        })
         .when('/PerfilConfig', {
             controller: 'perfilConfigController',
             templateUrl: 'PerfilConfig/Index'
         })
        .when('/PerfilConfig/Editar/:perfilId', {
            controller: 'perfilConfigController',
            templateUrl: function (params) { return 'PerfilConfig/Editar/' + params.perfilId; }
        })
        .when('/MeuPerfil/Editar', {
            controller: 'perfilConfigController',
            templateUrl: 'EditarPerfil/Index'
        })
        .when('/AreaConfig', {
            controller: 'areaConfigController',
            templateUrl: 'AreaConfig/Index'
        })
        .when('/AreaConfig/Editar/:areaId', {
            controller: 'areaConfigController',
            templateUrl: function (params) { return 'AreaConfig/Editar/' + params.areaId; }
        })
        .when('/GrupoDocumentoConfig', {
            controller: 'grupoDocumentoConfigController',
            templateUrl: 'GrupoDocumentoConfig/Index'
        })
        .when('/GrupoDocumentoConfig/Editar/:tipodocumentoId', {
            controller: 'grupoDocumentoConfigController',
            templateUrl: function (params) { return 'GrupoDocumentoConfig/Editar/' + params.tipodocumentoId; }
        })
        .when('/TipoDocumentoConfig', {
            controller: 'tipoDocumentoConfigController',
            templateUrl: 'TipoDocumentoConfig/Index'
        })
        .when('/TipoDocumentoConfig/Editar/:tipodocumentoId', {
            controller: 'tipoDocumentoConfigController',
            templateUrl: function (params) { return 'TipoDocumentoConfig/Editar/' + params.tipodocumentoId; }
        })
        .when('/ChaveConfig/:tipodocumentoId', {
            controller: 'chaveConfigController',
            templateUrl: function (params) { return 'ChaveConfig/' + params.tipodocumentoId; }
        })
        .when('/ChaveConfig/Editar/:chaveid', {
            controller: 'chaveConfigController',
            templateUrl: function (params) { return 'ChaveConfig/Editar/' + params.chaveid; }
        })
        .when('/ChaveConfig/Adicionar/:tipodocumentoId', {
            controller: 'chaveConfigController',
            templateUrl: function (params) { return 'ChaveConfig/Adicionar/' + params.tipodocumentoId; }
        })
        .when('/AcessoFuncionalidade', {
            controller: 'acessoFuncionalidadeController',
            templateUrl: 'AcessoFuncionalidadeConfig/Index'
        })
        .when('/AcessoDocumento', {
            controller: 'acessoDocumentoController',
            templateUrl: 'AcessoDocumentoConfig/Index'
        })
        .when('/Configuracoes', {
            controller: 'viewController',
            templateUrl: 'Configuracao/Index'
        })
        .when('/PesquisaView/Resultado/:termo', {
            controller: 'pesquisaController',
            templateUrl: function (params) { return 'Pesquisa/Resultado/' + params.termo; }
        })
        .when('/View/:documentoId', {
            controller: 'viewController',
            templateUrl: function (params) { return 'View/' + params.documentoId; }
        })
        .otherwise({ redirectTo: '/Login' });
}