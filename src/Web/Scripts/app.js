angular.module("salus-app", ["ngMessages", "ngRoute"])
    .config(config);
    
function config($routeProvider, $locationProvider) {
    var viewBase = 'views/';
    $routeProvider
        .when('/Home', {
            controller: 'homeController',
            templateUrl: "Home/Me",
        })
        .when('/Login', {
            controller: 'loginController',
            templateUrl: 'Login/Index'
        })
        .otherwise({ redirectTo: '/Login' });
}