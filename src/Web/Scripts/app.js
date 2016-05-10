angular.module("salus-app", [
                    "ngMessages", 
                    "ngRoute", 
                    "ui.bootstrap",
                    "ngFileUpload",
                    "ngCookies",
                    "snap"])
    .config(config);
    
function config($routeProvider, $locationProvider) {
    var viewBase = '/UserInterface/';
    $routeProvider
        .when('/Home', {
            controller: 'homeController',
            templateUrl: viewBase + 'Home/Me.cshtml',
        })
        .when('/Login', {
            controller: 'loginController',
            templateUrl: viewBase + 'Login/Index.cshtml'
        })
        .when('/Upload', {
            controller: 'uploadController',
            templateUrl: viewBase + 'Documento/Novo.cshtml'
        })
        .when('/Categorizar/:documentoId', {
            controller: 'categorizacaoController',
            templateUrl: viewBase + 'Categorizar/Index.cshtml'
        })
        .when('/UsuarioConfig', {
            controller: 'usuarioConfigController',
            templateUrl: viewBase + 'UsuarioConfig/Index.cshtml'
        })
        .when('/UsuarioConfig/Editar/:usuarioId', {
            controller: 'usuarioConfigController',
            templateUrl: viewBase + 'UsuarioConfig/Editar.cshtml'
        })
         .when('/PerfilConfig', {
             controller: 'perfilConfigController',
             templateUrl: viewBase + 'PerfilConfig/Index'
         })
        .when('/PerfilConfig/Editar/:perfilId', {
            controller: 'perfilConfigController',
            templateUrl: viewBase + 'PerfilConfig/Editar.cshtml'
        })
        .when('/MeuPerfil/Editar', {
            controller: 'perfilConfigController',
            templateUrl: viewBase + 'EditarPerfil/Index'
        })
        .when('/AreaConfig', {
            controller: 'areaConfigController',
            templateUrl: viewBase + 'AreaConfig/Index'
        })
        .when('/AreaConfig/Editar/:areaId', {
            controller: 'areaConfigController',
            templateUrl: viewBase + 'AreaConfig/Editar.cshtml'
        })
        .when('/GrupoDocumentoConfig', {
            controller: 'grupoDocumentoConfigController',
            templateUrl: viewBase + 'GrupoDocumentoConfig/Index.cshtml'
        })
        .when('/GrupoDocumentoConfig/Editar/:grupodocumentoId', {
            controller: 'grupoDocumentoConfigController',
            templateUrl: viewBase + 'GrupoDocumentoConfig/Editar.cshtml'
        })
        .when('/TipoDocumentoConfig', {
            controller: 'tipoDocumentoConfigController',
            templateUrl: viewBase + 'TipoDocumentoConfig/Index.cshtml'
        })
        .when('/TipoDocumentoConfig/Editar/:tipodocumentoId', {
            controller: 'tipoDocumentoConfigController',
            templateUrl: viewBase + 'TipoDocumentoConfig/Editar.cshtml'
        })
        .when('/ChaveConfig/:tipodocumentoId', {
            controller: 'chaveConfigController',
            templateUrl: viewBase + 'ChaveConfig/Index.cshtml'
        })
        .when('/ChaveConfig/Editar/:chaveid', {
            controller: 'chaveConfigController',
            templateUrl: viewBase + 'ChaveConfig/Editar.cshtml'
        })
        .when('/ChaveConfig/Adicionar/:tipodocumentoId', {
            controller: 'chaveConfigController',
            templateUrl: viewBase + 'ChaveConfig/Adicionar.cshtml'
        })
        .when('/AcessoFuncionalidade', {
            controller: 'acessoFuncionalidadeController',
            templateUrl: viewBase + 'AcessoFuncionalidadeConfig/Index.cshtml'
        })
        .when('/AcessoDocumento', {
            controller: 'acessoDocumentoController',
            templateUrl: viewBase + 'AcessoDocumentoConfig/Index.cshtml'
        })
        .when('/Configuracoes', {
            controller: 'viewController',
            templateUrl: viewBase + 'Configuracao/Index.cshtml'
        })
        .when('/PesquisaView/Resultado/:termo', {
            controller: 'pesquisaController',
            templateUrl: viewBase + 'Pesquisa/Resultado.cshtml'
        })
        .when('/View/:documentoId', {
            controller: 'viewController',
            templateUrl: viewBase + 'View/Index.cshtml'
        })
        .otherwise({ redirectTo: '/Login' });
}