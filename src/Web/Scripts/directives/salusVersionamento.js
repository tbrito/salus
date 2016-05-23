angular.module('salus-app').directive('salusVersionamento', function (Upload, versaoDocumentoApi) {
    return {
        templateUrl: "/UserInterface/Versionamento/_versaoDocumento.html",
        restrict: 'AE',
        scope: {
            versionados: '@',
            documentocode: '@',
            bloqueado: '@'
        },
        link: function (scope, element, attrs, ctrl) {
            scope.versionamentoCtrl = function () {
                var checkout = function (documentoId) {
                    versaoDocumentoApi.checkout(documentoId)
                        .success(function (data) {
                            $scope.urlDocumentoDownload = data.UrlDocumento;
                        })
                        .error(function (data) {
                            console.log(data);
                        });
                }
            }

            scope.finalizarCheckout = function () {
                scope.finalizarVersionamento = true;
            }

            scope.checkin = function (documentoId) {
                versaoDocumentoApi.checkout(documentoId)
                    .success(function (data) {
                        $scope.urlDocumentoDownload = data.urlDownload;
                    })
                    .error(function (data) {
                        console.log(data);
                    });
            }

            scope.enviarNovaVersao = function (file, versao) {
                versao.Documento = { Id: scope.documentocode };
                versaoDocumentoApi.checkin(versao)
                    .success(function (data) {
                        $scope.versoes = data.Versoes;
                        $scope.versaoId = data.VersaoId;
                    })
                    .error(function (data) {
                        $scope.error = "Ops! Algo aconteceu ao versionar documento ";
                    });

                if (file == undefined) {
                    return;
                }

                file.name = file;

                file.upload = Upload.upload({
                    url: 'api/Arquivos/AddVersao/' + scope.documentocode,
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
        }
    }
});