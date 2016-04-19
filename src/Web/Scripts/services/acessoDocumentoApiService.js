angular.module("salus-app").factory("acessoDocumentoApi", function ($http) {
    
    var _postSalvarAcesso = function (acessoDocumento) {
        return $http.post("api/AcessoDocumento/Salvar/", acessoDocumento);
    };

    var _getObterAcesso = function (viewModel) {
        return $http({
            method: 'GET',
            url: 'api/AcessoDocumento/ObterPor',
            params: { atorId: viewModel.AtorId, papelId: viewModel.PapelId }
         });
    };
    
    return {
        postSalvarAcesso: _postSalvarAcesso,
        getObterAcesso: _getObterAcesso
    };
});