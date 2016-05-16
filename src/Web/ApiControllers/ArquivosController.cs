﻿namespace Web.ApiControllers
{
    using Extensions;
    using Filters;
    using Salus.Infra;
    using Salus.Infra.IoC;
    using Salus.Infra.Transformations;
    using Salus.Model;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using Salus.Model.UI;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class ArquivosController : ApiController
    {
        private readonly ISessaoDoUsuario sessaoDoUsuario;
        private readonly SalvarConteudoServico salvarConteudoServico;
        private readonly StorageServico storageServico;
        private readonly IMongoStorage mongoStorage;
        private readonly OpenOfficeTransformer openOfficeTransformer;

        public ArquivosController()
        {
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
            this.salvarConteudoServico = InversionControl.Current.Resolve<SalvarConteudoServico>();
            this.storageServico = InversionControl.Current.Resolve<StorageServico>();
            this.mongoStorage = InversionControl.Current.Resolve<IMongoStorage>();
            this.openOfficeTransformer = InversionControl.Current.Resolve<OpenOfficeTransformer>();
        }

        [HttpGet]
        public IHttpActionResult Documento(int id)
        {
            var caminho = string.Empty;
            var caminhoPdf = string.Empty;

            caminho = this.storageServico.Obter("[documento]" + id.ToString());

            if (Path.GetExtension(caminho).ToLower() == ".pdf")
            {
                caminhoPdf = caminho;
            }
            else
            {
                caminhoPdf = Path.Combine(
                    Path.GetDirectoryName(caminho),
                    Path.GetFileNameWithoutExtension(caminho) + ".pdf");

                this.openOfficeTransformer.Execute(caminho, caminhoPdf);
            }
            
            var relativo = caminhoPdf
                .Replace(Aplicacao.Caminho, string.Empty)
                .Replace(@"\", "/");

            return Ok(new { urlDocumento = "/" + relativo });
        }

        [HttpPost]
        public async Task<IHttpActionResult> Add(int id)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Tipo não suportado");
            }
            try
            {
                SynchronizationContext originalContext = SynchronizationContext.Current;

                var pastaTrabalho = Path.Combine(
                    ContextoWeb.Caminho,
                    "Uploads",
                    this.sessaoDoUsuario.UsuarioAtual.Id.ToString());

                var provider = new CustomMultipartFormDataStreamProvider(pastaTrabalho);

                if (Directory.Exists(pastaTrabalho) == false)
                {
                    Directory.CreateDirectory(pastaTrabalho);
                }

                await Request.Content.ReadAsMultipartAsync(provider);

                var arquivos =
                  provider.FileData
                    .Select(file => new FileViewModel
                    {
                        Name = Path.GetFileNameWithoutExtension(file.LocalFileName),
                        Created = DateTime.Now,
                        Path = file.LocalFileName,
                        Subject = provider.FormData["assunto"]
                    }).ToList();
                
                IList<Documento> documentos = new List<Documento>();

                originalContext.Post(
                    delegate {
                        documentos = this.salvarConteudoServico.Executar(arquivos);}, null);

                Request.Content.Dispose();

                return Ok(new { Message = "Documentos enviados com sucesso", Documentos = documentos });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddFoto(int id)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Tipo não suportado");
            }
            try
            {
                SynchronizationContext originalContext = SynchronizationContext.Current;

                var usuario = this.sessaoDoUsuario.UsuarioAtual;
                var usuarioId = usuario.Id.ToString();

                var pastaTrabalho = Path.Combine(
                    ContextoWeb.Caminho,
                    "Uploads",
                    usuario.Id.ToString());

                var provider = new CustomMultipartFormDataStreamProvider(pastaTrabalho);

                if (Directory.Exists(pastaTrabalho) == false)
                {
                    Directory.CreateDirectory(pastaTrabalho);
                }

                await Request.Content.ReadAsMultipartAsync(provider);

                var arquivos =
                  provider.FileData
                    .Select(file => new FileInfo(file.LocalFileName))
                    .Select(fileInfo => new FileViewModel
                    {
                        Name = fileInfo.Name,
                        Created = fileInfo.CreationTime,
                        Modified = fileInfo.LastWriteTime,
                        Size = fileInfo.Length / 1024,
                        Path = fileInfo.FullName
                    }).ToList();

                originalContext.Post(
                    delegate {
                        this.salvarConteudoServico.SalvarFoto(arquivos, "[usuario]" + usuarioId);
                    }, null);

                Request.Content.Dispose();

                return Ok(new { Message = "Documentos enviados com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }


        /// <summary>
        ///   Check if file exists on disk
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FileExists(string fileName)
        {
            var file = Directory.GetFiles(ContextoWeb.Caminho, fileName)
              .FirstOrDefault();

            return file != null;
        }
    }
}