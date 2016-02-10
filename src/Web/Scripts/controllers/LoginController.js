angular.module("salus-app").controller("LoginController", function ($scope, usuarioApi, $location, authenticationService){
    $scope.usuario = {};
    
    $scope.login = function (usuario) {
        usuarioApi.getGetUsuario(usuario)
        .success(function (data) {
            $scope.usuario = data;

            authenticationService.SetCredentials(data.username, data.password);
            $location.path('/');
        })
        .error( function(data){
            $scope.error = "Ops! Algo aconteceu ao tentar autenticação";  
        });
    };
});