angular.module('salus-app').directive('salusMesano', function ($filter) {
    return {
        require: "ngModel",
        link: function (scope, element, attrs, ctrl) {
            var _formatDate = function (date) {
                date = date.replace(/[^0-9]+/g, "");
                if (date.length > 2) {
                    date = date.substring(0, 2) + "/" + date.substring(2);
                }

                return date;
            };

            element.bind("keyup", function () {
                ctrl.$setViewValue(_formatDate(ctrl.$viewValue));
                ctrl.$render();
            });

            ctrl.$parsers.push(function (value) {
                if (value.length === 7) {
                    var dateArray = value.split("/");
                    return new Date(dateArray[1], dateArray[0], 1).getTime();
                }
            });

            ctrl.$formatters.push(function (value) {
                return $filter("date")(value, "MM/yyyy");
            });
        }
    };
});