angular.module("salus-app").controller('perfilConfigController', function ($scope, $location, perfilApi) {

    $scope.perfis = [];
    
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
    
    $scope.carregarParaEdicao = function (perfilId) {
        $scope.perfil = {};

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

    $scope.excluir = function (grupodocumentoid) {
        grupoDocumentoApi.excluir(grupoDocumentid)
            .success(function (data) {
                $location.path('/GrupoDocumentoConfig');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.inicio = function () {
        $location.path('/PerfilConfig');
    }
});