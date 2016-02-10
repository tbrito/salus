angular.module("salus-app").controller("LoginController", function ($scope, usuarioApi, $location) {
    $scope.usuario = {};
    
    $scope.login = function (usuario) {
        usuarioApi.getGetUsuario(usuario)
        .success(function (data) {
            $scope.usuario = data;
            console.log(data);
            ////authenticationService.SetCredentials(data.UserName, data.Senha);
            $location.path('/');
        })
        .error( function(data){
            $scope.error = "Ops! Algo aconteceu ao tentar autenticação";  
        });
    };
});