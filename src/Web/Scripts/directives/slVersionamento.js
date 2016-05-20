(function () {
    'use strict';
    angular.module('salus-app').directive('slVersionamento', function () {
        return {
            templateUrl: "/UserInterface/Versionamento/_versaoDocumento.html",
            restrict: 'AEC',
            scope: {
                versoes: '@versoes',
                documentoId: '@documentoId',
                bloqueado: '@bloqueado'
            }
        };
    })
})();