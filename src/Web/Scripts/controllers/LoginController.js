angular.module("salus-app").controller("loginController", function ($scope, usuarioApi, $location) {
    //$scope.usuario = {};
    
    $scope.login = function (usuario) {
        usuarioApi.getUserLogin(usuario)
            .success(function (data) {
                $scope.usuario = data;

                if ($scope.usuario.autenticado) {
                    $location.path('/Home');
                } else {
                    $scope.error = "Usuário ou senha inválidos.";
                }
            })
            .error( function(data){
                $scope.error = "Ops! Algo aconteceu ao tentar autenticação";
            });
    };
});