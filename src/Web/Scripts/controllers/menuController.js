angular.module('salus-app').controller('menuControler', function ($scope, $log, $location) {
  $scope.items = [
    'The first choice!',
    'And another choice for you.',
    'but wait! A third!'
  ];

  $scope.status = {
    isopen: false
  };

  $scope.toggled = function(open) {
    $log.log('Dropdown is now: ', open);
  };

  $scope.toggleDropdown = function($event) {
    $event.preventDefault();
    $event.stopPropagation();
    $scope.status.isopen = !$scope.status.isopen;
  };

  $scope.appendToEl = angular.element(document.querySelector('#dropdown-long-content'));
  
  $scope.usuario = {};

  $scope.start = function () {
      $scope.usuario = {
          avatar: 'https://avatars0.githubusercontent.com/u/737150?v=3&amp;s=40',
          autenticado: false
      };
  }

  $scope.abrirUpload= function() {
    $location.path('/Upload');
  };

  $scope.abrirHome = function () {
      $location.path('/Home');
  };

  $scope.abrirTiposDeDocumento = function () {
      $location.path('/TipoDocumentoConfig');
  };

   $scope.abrirGruposDeDocumento = function () {
      $location.path('/GrupoDocumentoConfig');
  };

   $scope.abrirAreas = function () {
       $location.path('/AreaConfig');
   };

   $scope.abrirUsuarios = function () {
       $location.path('/UsuarioConfig');
   };

   $scope.abrirPerfis = function () {
       $location.path('/PerfilConfig');
   };

   $scope.abrirSegurancaDocumentos = function () {
       $location.path('/AcessoDocumento');
   };

   $scope.abrirSegurancaFuncionalidades = function () {
       $location.path('/AcessoFuncionalidade');
   };
});