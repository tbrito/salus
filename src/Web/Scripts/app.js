angular.module("salus-app", ["ngMessages", "ngRoute"])
    .config(config);
    
function config($routeProvider, $locationProvider){
    $routeProvider
        .when('/', {
            controller: 'HomeController',
            templateUrl: '~/Views/Home/Index.cshtml',
        })
        
        .when('/login', {
            controller: 'LoginController',
            templateUrl: 'views/login/index.cshtml'
        })
        
        .when('/register', {
            controller: 'RegisterController',
            templateUrl: 'views/register/register.html'
        })
        
        .otherwise({ redirectTo: '/login' });
}