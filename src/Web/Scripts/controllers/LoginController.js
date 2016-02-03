angular.module("salus-app").controller("LoginController", function ($scope, usuarioApi){
    $scope.usuario = {};
    
    $scope.login = function (usuario) {
        usuarioApi.getGetUsuario(usuario)
        .success(function (data) {
            $scope.usuario = data;
        })
        .error( function(data){
            $scope.error = "Ops! Algo aconteceu ao tentar autenticação";  
        });
    };
});