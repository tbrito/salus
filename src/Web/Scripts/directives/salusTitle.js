angular.module('salus-app').directive('salusTitle', function () {
    return {
        templateUrl: "/UserInterface/Title/_title.html",
        restrict: 'AE',
        transclude: true
    };
});