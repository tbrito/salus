angular.module("salus-app", ["ngMessages", "ngRoute", "ui.bootstrap"])
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
        .otherwise({ redirectTo: '/Login' });
}