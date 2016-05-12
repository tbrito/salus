angular.module("salus-app").controller('usuarioConfigController',
        function ($scope, $location, areaApi, perfilApi, usuarioApi, $routeParams) {

    $scope.usuarios = [];
    $scope.areas = [];
    $scope.perfis = [];
    
    $scope.adicionar = function () {
        $location.path('/UsuarioConfig/Editar/' + 0);
    }

    $scope.carregarFormulario = function () {

        usuarioApi.getUsuarios()
            .success(function (data) {
                $scope.usuarios = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os usuarios";
            });
    }

    $scope.editarUsuario = function (usuarioid) {
        $location.path('/UsuarioConfig/Editar/' + usuarioid);
    }
    
    $scope.carregarParaEdicao = function () {
        var usuarioid = $routeParams.usuarioId;
        if (usuarioid == 0) {
            $scope.usuario = { Id: 0 };
            
            areaApi.getAreas()
                .success(function (data) {
                    $scope.areas = data;
                })
                .error(function (data) {
                    $scope.error = "Não foi possível carregar areas." + data;
                });

            perfilApi.getPerfis()
                .success(function (data) {
                    $scope.perfis = data;
                });
        }
        else {
            usuarioApi.getUsuario(usuarioid)
                .success(function (data) {
                    $scope.usuario = data;

                    areaApi.getAreas()
                        .success(function (data) {
                            $scope.areas = data;
                        });

                    perfilApi.getPerfis()
                        .success(function (data) {
                            $scope.perfis = data;
                        });
                    })
                .error(function (data) {
                    $scope.error = "Ops! Algo aconteceu ao obter o usuario";
                });
        }
    }

    $scope.salvar = function (usuario) {
        usuarioApi.salvar(usuario)
            .success(function (data) {
                $location.path('/UsuarioConfig');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao salvar o usuario";
            });
    }

    $scope.excluir = function (usuario) {
        usuarioApi.excluir(usuario)
            .success(function (data) {
                usuario.Ativo = false;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao inativar o usuario" + data;
            });
    }

    $scope.ativar = function (usuario) {
        usuarioApi.ativar(usuario)
            .success(function (data) {
                usuario.Ativo = true;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao inativar o usuario" + data;
            });
    }

    $scope.inicio = function () {
        $location.path('/UsuarioConfig');
    }
});