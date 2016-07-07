using System;
using System.Collections.Generic;
using Salus.Model.Entidades;

namespace Salus.Model.UI
{
    public class AreaViewModel
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Abreviacao { get; set; }
        public bool Segura { get; set; }
        public AreaViewModel Parent { get; set; }
        public IList<AreaViewModel> SubAreas { get; set; }

        public static AreaViewModel Criar(Area area)
        {
            var viewModel = new AreaViewModel
            {
                Id = area.Id,
                Abreviacao = area.Abreviacao,
                Ativo = area.Ativo,
                Nome = area.Nome,
                Segura = area.Segura
            };

            if (area.Parent != null)
            {
                viewModel.Parent = Criar(area.Parent);
            }

            foreach (var item in area.SubAreas)
            {
                viewModel.SubAreas.Add(Criar(item));
            }
            return viewModel;
        }
    }
}