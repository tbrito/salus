angular.module("salus-app")
    .controller('uploadController', ['$scope', 'Upload', '$timeout', '$location', function ($scope, Upload, $timeout, $location) {

        $scope.uploadPic = function (file) {

            file.name = file;

            file.upload = Upload.upload({
                url: 'api/Files/Add',
                data: { file: file, username: $scope.username },
            });d

            file.upload.then(function (response) {
                $timeout(function () {
                    file.result = response.data;
                    $location.path('/Categorizar/' + response.data.documentos[0].id);
                });
            }, function (response) {
                if (response.status > 0)
                    $scope.errorMsg = response.status + ': ' + response.data;
            }, function (evt) {
                // Math.min is to fix IE which reports 200% sometimes
                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
            });
        }
    }]);