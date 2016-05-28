angular.module("salus-app").controller('chaveConfigController',
    function ($scope, $location, chavesApi, $routeParams) {

        $scope.tiposDeDado = [
            { nome: "Texto", id: 0 },
            { nome: "Inteiro", id: 1 },
            { nome: "Real", id: 2 },
            { nome: "Data", id: 3 },
            { nome: "Lista", id: 8 },
            { nome: "Regex", id: 9 },
            { nome: "Cpf Cnpj", id: 17 },
            { nome: "Ano Referencia", id: 15 },
            { nome: "Mes Ano", id: 32 },
            { nome: "Area", id: 18 }
        ];

        $scope.retornarTipoDado = function (tipoDado) {
            var tipoDadoSelecionado = $scope.tiposDeDado.filter(function (dado) {
                return dado.id == tipoDado;
            })

            return tipoDadoSelecionado[0].nome;
        }

        $scope.adicionarChave = function (tipodocumentoId, nome) {
            $location.path('/ChaveConfig/Adicionar/' + tipodocumentoId + '/' + nome);
        }

        $scope.carregarFormularioDeChaves = function () {
            $scope.tipodocumentoId = $routeParams.tipodocumentoid;
            $scope.tipoDocumentoNome = $routeParams.tipodocumentonome;

            chavesApi.getChavesDoTipo($scope.tipodocumentoId)
                .success(function (data) {
                    $scope.chaves = data;
                })
                .error(function (data) {
                    $scope.error = "Ops! Algo aconteceu ao obter as chaves";
                });
        }

        $scope.editarChave = function (chave) {
            $location.path('/ChaveConfig/Alterar/' + chave.Id);
        }

        $scope.configurarCampos = function (chave) {
            if (chave.TipoDado == 8) {
                chave.EhLista = true;
            } else {
                chave.EhLista = false;
            }
        }

        $scope.carregarChaveParaEdicao = function () {
            if ($routeParams.chaveid == undefined) {
                $scope.tipodocumentoId = $routeParams.tipodocumentoid;
                $scope.tipoDocumentoNome = $routeParams.tipodocumentonome;
            }
            else {
                var chaveid = $routeParams.chaveid;
            }

            if (chaveid != 0) {
                chavesApi.getChave(chaveid)
                    .success(function (data) {
                        $scope.chave = data;
                    })
                    .error(function (data) {
                        $scope.error = "Ops! Algo aconteceu ao obter a chave";
                    });
            }
        }

        $scope.salvar = function (chave) {
            if ($scope.tipodocumentoId == undefined) {
                chave.TipoDocumentoId = chave.TipoDocumento.id
            } else {
                chave.TipoDocumentoId = $scope.tipodocumentoId;
            }

            chavesApi.salvar(chave)
                .success(function (data) {
                    $location.path('/ChaveConfig/TodosDoTipo/' + data.TipoDocumentoId + '/' + data.TipoDocumentoNome);
                })
                .error(function (data) {
                    $scope.error = "Ops! Algo aconteceu ao salvar as chaves do tipo do documento";
                });
        }

        $scope.excluir = function (chave) {
            chavesApi.excluir(chave)
                .success(function (data) {
                    $location.path('/ChaveConfig/' + chave.Id);
                })
                .error(function (data) {
                    $scope.error = "Ops! Algo aconteceu ao obter as chaves do tipo do documento";
                });
        }

        $scope.inicio = function () {
            $location.path('/TipoDocumentoConfig');
        }
    });