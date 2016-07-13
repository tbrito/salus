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
                Segura = area.Segura,
                SubAreas = new List<AreaViewModel>()
            };

            if (area.Parent != null)
            {
                viewModel.Parent = CriarParent(area.Parent);
            }

            foreach (var item in area.SubAreas)
            {
                viewModel.SubAreas.Add(CriarSubArea(item));
            }

            return viewModel;
        }

        private static AreaViewModel CriarSubArea(Area area)
        {
            var viewModel = new AreaViewModel
            {
                Id = area.Id,
                Abreviacao = area.Abreviacao,
                Ativo = area.Ativo,
                Nome = area.Nome,
                Segura = area.Segura
            };

            foreach (var item in area.SubAreas)
            {
                viewModel.SubAreas.Add(CriarSubArea(item));
            }

            return viewModel;
        }

        private static AreaViewModel CriarParent(Area parent)
        {
            var viewModel = new AreaViewModel
            {
                Id = parent.Id,
                Abreviacao = parent.Abreviacao,
                Ativo = parent.Ativo,
                Nome = parent.Nome,
                Segura = parent.Segura
            };

            if (parent.Parent != null)
            {
                viewModel.Parent = CriarParent(parent.Parent);
            }

            return viewModel;
        }
    }
}