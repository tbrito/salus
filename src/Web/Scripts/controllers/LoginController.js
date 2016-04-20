angular.module("salus-app").controller("loginController", function ($scope, usuarioApi, autorizacaoApi, $location) {
    
    $scope.login = function (usuario) {
        usuarioApi.getUserLogin(usuario)
            .success(function (data) {
                $scope.usuario = data;

                if ($scope.usuario.Autenticado) {
                    autorizacaoApi.salvar($scope.usuario);
                    
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