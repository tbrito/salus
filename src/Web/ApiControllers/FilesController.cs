namespace Web.ApiControllers
{
    using Extensions;
    using Salus.Infra.IoC;
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
    using System.Threading.Tasks;
    using System.Web.Http;

    public class FilesController : ApiController
    {
        private ISessaoDoUsuario sessaoDoUsuario;
        private SalvarConteudoServico salvarConteudoServico;
        private StorageServico storageServico;
        private IMongoStorage mongoStorage;

        public FilesController()
        {
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
            this.salvarConteudoServico = InversionControl.Current.Resolve<SalvarConteudoServico>();
            this.storageServico = InversionControl.Current.Resolve<StorageServico>();
            this.mongoStorage = InversionControl.Current.Resolve<IMongoStorage>();
        }

        /// <summary>
        ///   Get all photos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Documento(int documentoId)
        {
            ////await Task.Factory.StartNew(() =>
            ////{
                var caminho = this.storageServico.Obter(documentoId);
            ////});

            return Ok(new { Caminho = caminho });
        }

        /// <summary>
        ///   Delete photo
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string fileName)
        {
            if (!FileExists(fileName))
            {
                return NotFound();
            }

            try
            {
                var filePath = Directory.GetFiles(string.Empty, fileName)
                  .FirstOrDefault();

                await Task.Factory.StartNew(() =>
                {
                    if (filePath != null)
                        File.Delete(filePath);
                });

                var result = new PhotoActionResult
                {
                    Successful = true,
                    Message = fileName + "deleted successfully"
                };
                return Ok(new { message = result.Message });
            }
            catch (Exception ex)
            {
                var result = new PhotoActionResult
                {
                    Successful = false,
                    Message = "error deleting fileName " + ex.GetBaseException().Message
                };
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        ///   Add a photo
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Add()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Tipo não suportado");
            }
            try
            {
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
                    .Select(file => new FileInfo(file.LocalFileName))
                    .Select(fileInfo => new FileViewModel
                    {
                        Name = fileInfo.Name,
                        Created = fileInfo.CreationTime,
                        Modified = fileInfo.LastWriteTime,
                        Size = fileInfo.Length / 1024,
                        Path = fileInfo.FullName,
                        Subject = provider.FormData["username"]
                    }).ToList();

                IList<Documento> documentos = new List<Documento>();

                ////await Task.Factory.StartNew(() =>
                ////{
                documentos = this.salvarConteudoServico.Executar(arquivos);
                ////});

                return Ok(new { Message = "Documentos enviados com sucesso", Documentos = documentos });
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