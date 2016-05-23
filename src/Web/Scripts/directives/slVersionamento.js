(function () {
    'use strict';
    angular.module('salus-app').directive('slVersionamento', function (Upload, versaoDocumentoApi) {
        return {
            templateUrl: "/UserInterface/Versionamento/_versaoDocumento.html",
            controller: versionamentoCtrl,
            restrict: 'AEC',
            scope: {
                versoes: '@versoes',
                documentoId: '@documentoId',
                bloqueado: '@bloqueado'
            }
        };
    })

    var versionamentoCtrl = function () {
        var checkout = function (documentoId) {
            versaoDocumentoApi.checkout(documentoId)
                .success(function (data) {
                    $scope.urlDocumentoDownload = data.UrlDocumento;
                })
                .error(function (data) {
                    console.log(data);
                });

        }

        var finalizarCheckout = function () {
            $scope.finalizarVersionamento = true;
        }

        var checkin = function (documentoId) {
            versaoDocumentoApi.checkout(documentoId)
                .success(function (data) {
                    $scope.urlDocumentoDownload = data.urlDownload;
                })
                .error(function (data) {
                    console.log(data);
                });
        }

        var enviarNovaVersao = function (file, versao) {
            versaoApi.salvar(versao)
                .success(function (data) {
                    $scope.versoes = data;
                })
                .error(function (data) {
                    $scope.error = "Ops! Algo aconteceu ao versionar documento ";
                });

            if (file == undefined) {
                return;
            }

            file.name = file;

            file.upload = Upload.upload({
                url: 'api/Arquivos/AddVersao/' + $scope.documentoId,
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
})();