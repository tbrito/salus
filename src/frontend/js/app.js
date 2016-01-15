angular.module("salus-app", ["ngMessages", "ngRoute"])
    .config(config);
    
function config($routeProvider, $locationProvider){
    $routeProvider
        .when('/', {
            controller: 'HomeController',
            templateUrl: 'views/home/home.html',
        })
        
        .when('/login', {
            controller: 'LoginController',
            templateUrl: 'views/login/login.html'
        })
        
        .when('/register', {
            controller: 'RegisterController',
            templateUrl: 'views/register/register.html'
        })
        
        .otherwise({ redirectTo: '/login' });
}