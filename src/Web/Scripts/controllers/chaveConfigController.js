angular.module("salus-app").controller('chaveConfigController', function ($scope, $location, chavesApi) {

    $scope.chaves = [];

    $scope.chave = {};

    $scope.tiposDeDado = [
        { nome: "Texto", id: 0 },
        { nome: "Mascara", id: 1 },
        { nome: "Inteiro", id: 2 },
        { nome: "Moeda", id: 3 },
        { nome: "Lista", id: 4 },
        { nome: "CpfCnpj", id: 5 }
    ]

    $scope.adicionar = function () {
        $location.path('/ChaveConfig/Editar/' + 0);
    }

    $scope.carregarFormulario = function (tipodocumentoid, tipodocumentonome) {
        $scope.tipodocumentoId = tipodocumentoid;
        $scope.tipoDocumentoNome = tipodocumentonome;

        chavesApi.getChavesDoTipo(tipodocumentoid)
            .success(function (data) {
                $scope.chaves = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as chaves";
            });
    }

    $scope.editarChave = function (chaveid) {
        $location.path('/ChaveConfig/Editar/' + chaveid);
    }

    $scope.configurarCampos = function () {
        if (tipoDado.id == 4) {
            $scope.chave.ehLista = true;
        } else {
            $scope.chave.ehLista = false;
        }
    }

    $scope.carregarParaEdicao = function (chaveid) {
        if (chaveid != 0) {
            chavesApi.getChave(chaveid)
                .success(function (data) {
                    $scope.chave = data;
                    $scope.chave.tiposDeDado = [
                        { nome: "Texto", id: 0 },
                        { nome: "Mascara", id: 1 },
                        { nome: "Inteiro", id: 2 },
                        { nome: "Moeda", id: 3 },
                        { nome: "Lista", id: 4 },
                        { nome: "CpfCnpj", id: 5 }
                    ]
                })
                .error(function (data) {
                    $scope.error = "Ops! Algo aconteceu ao obter a chave";
                });
        }
    }

    $scope.salvar = function (chave) {
        chavesApi.salvar(chave)
            .success(function (data) {
                $location.path('/ChaveConfig/' + tipodocumentoId);
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao salvar as chaves do tipo do documento";
            });
    }

    $scope.excluir = function (chaveid) {
        chavesApi.excluir(chaveid)
            .success(function (data) {
                $location.path('/ChaveConfig/' + tipodocumentoId);
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter as chaves do tipo do documento";
            });
    }

    $scope.inicio = function () {
        $location.path('/TipoDocumentoConfig');
    }
});