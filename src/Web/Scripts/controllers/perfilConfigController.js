angular.module("salus-app").controller('perfilConfigController',
    ['$scope', 'Upload', '$timeout', '$location', 'perfilApi', '$routeParams',
        function ($scope, Upload, $timeout, $location, perfilApi, $routeParams) {

    $scope.perfis = [];
    $scope.perfil = {};

    $scope.adicionar = function () {
        $location.path('/PerfilConfig/Editar/' + 0);
    }

    $scope.carregarFormulario = function () {

        perfilApi.getPerfis()
            .success(function (data) {
                $scope.perfis = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os perfis do sistema" + data;
            });
    }

    $scope.editarPerfil = function (perfilId) {
        $location.path('/PerfilConfig/Editar/' + perfilId);
    }
    
    $scope.carregarParaEdicao = function () {
        var perfilId = $routeParams.perfilId;
       
        if (perfilId != 0) {
            perfilApi.getPerfil(perfilId)
                .success(function (data) {
                    $scope.perfil = data;
                })
                .error(function (data) {
                    $scope.error = "Ops! Algo aconteceu ao adicionar/editar os perfis do sistema" + data;
                });
        }
    }

    $scope.salvar = function (perfil) {
        perfilApi.salvar(perfil)
            .success(function (data) {
                $location.path('/PerfilConfig');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao salvar o perfil " + data;
            });
    }

    $scope.salvarEdicaoPerfil = function (file, perfil) {

        perfilApi.salvarSenha(perfil)
            .success(function (data) {
                $scope.mensagem = "Perfil Atualizado com Sucesso";
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao salvar o perfil " + data;
            });

        if (file == undefined) {
            return;
        }

        file.name = file;

        file.upload = Upload.upload({
            url: 'api/Arquivos/AddFoto/0',
            data: { file: file },
        });

        file.upload.then(function (response) {
            $timeout(function () {
                file.result = response.data;
            });
        }, function (response) {
            if (response.status > 0)
                $scope.errorMsg = response.status + ': ' + response.data;
        }, function (evt) {
            file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
        });
    }

    $scope.excluir = function (perfil) {
        perfilApi.excluir(perfil)
            .success(function (data) {
                perfil.Ativo = false;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao desativar perfil";
            });
    }

     $scope.ativar = function (perfil) {
        perfilApi.ativar(perfil)
            .success(function (data) {
                perfil.Ativo = true;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao ativar o perfil";
            });
    }

    $scope.inicio = function () {
        $location.path('/PerfilConfig');
    }
}]);