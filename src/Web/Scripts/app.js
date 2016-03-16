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
        .when('/Indexacao/Categorizar/:documentoId', {
            controller: 'categorizacaoController',
            templateUrl: function (params) { return 'Indexacao/Categorizar/' + params.documentoId; }
        })
        .otherwise({ redirectTo: '/Login' });
}