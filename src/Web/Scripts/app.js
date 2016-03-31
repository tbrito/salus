angular.module("salus-app", [
                    "ngMessages", 
                    "ngRoute", 
                    "ui.bootstrap",
                    "ngFileUpload"])
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
        .when('/View/:documentoId', {
            controller: 'viewController',
            templateUrl: function (params) { return 'View/' + params.documentoId; }
        })
        .otherwise({ redirectTo: '/Login' });
}