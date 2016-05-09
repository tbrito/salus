angular.module("salus-app").controller('areaConfigController',
    function ($scope, $location, areaApi, $routeParams) {

    $scope.areas = [];
    
    $scope.adicionar = function () {
        $location.path('/AreaConfig/Editar/' + 0);
    }

    $scope.carregarFormulario = function () {

        areaApi.getAreas()
            .success(function (data) {
                $scope.areas = data;
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.editarArea = function (areaid) {
        $location.path('/AreaConfig/Editar/' + areaid);
    }
    
    $scope.carregarParaEdicao = function () {
        var areaid = $routeParams.areaId;

        if (areaid == 0) {
            $scope.area = {};
            
            areaApi.getAreas()
                .success(function (data) {
                    $scope.area.todas = data;
                });
        }
        else {
            areaApi.getArea(areaid)
                .success(function (data) {
                    $scope.area = data;
                    areaApi.getAreas()
                        .success(function (data) {
                            $scope.area.todas = data;
                        });
                })
                .error(function (data) {
                    $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
                });
        }
    }

    $scope.salvar = function (area) {
        areaApi.salvar(area)
            .success(function (data) {
                $location.path('/AreaConfig');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.excluir = function (areaid) {
        areaApi.excluir(areaid)
            .success(function (data) {
                $location.path('/AreaConfig');
            })
            .error(function (data) {
                $scope.error = "Ops! Algo aconteceu ao obter os tipos de documentos";
            });
    }

    $scope.inicio = function () {
        $location.path('/AreaConfig');
    }
});