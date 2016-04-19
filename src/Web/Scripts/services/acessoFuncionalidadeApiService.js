angular.module("salus-app").factory("acessoFuncionalidadeApi", function ($http) {
    
    var _postSalvarAcesso = function (acessoFuncionalidade) {
        return $http.post("api/AcessoFuncionalidade/Salvar/", acessoFuncionalidade);
    };

    var _getObterAcesso = function (viewModel) {
        return $http({
            method: 'GET',
            url: 'api/AcessoFuncionalidade/ObterPor',
            params: { atorId: viewModel.AtorId, papelId: viewModel.PapelId, }
         });
    };
    
    return {
        postSalvarAcesso: _postSalvarAcesso,
        getObterAcesso: _getObterAcesso
    };
});