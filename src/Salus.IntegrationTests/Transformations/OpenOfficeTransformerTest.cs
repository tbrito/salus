namespace Salus.IntegrationTests.Transformation.OpenOffice
{
    using Infra;
    using Infra.IO;
    using Infra.Transformations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class OpenOfficeTransformerTest 
    {
        private readonly OpenOfficeTransformer openOfficeTransformer = new OpenOfficeTransformer();
        private string docFile;
        private string xlsFile;
        private string docxFile;
        private string pptxFile;
        private string docFilePdf;
        private string xlsFilePdf;
        private string docxFilePdf;
        private string pptxFilePdf;

        [TestInitialize]
        public void Setup()
        {
            this.docFilePdf = Path.Combine(Aplicacao.Caminho, "Cenarios\\Arquivos\\word-01.pdf");
            this.xlsFilePdf = Path.Combine(Aplicacao.Caminho, "Cenarios\\Arquivos\\excel-01.pdf");
            this.docxFilePdf = Path.Combine(Aplicacao.Caminho, "Cenarios\\Arquivos\\word-02.pdf");
            this.pptxFilePdf = Path.Combine(Aplicacao.Caminho, "Cenarios\\Arquivos\\powerpoint.pdf");

            this.docFile = Path.Combine(Aplicacao.Caminho, "Cenarios\\Arquivos\\word-01.doc");
            this.xlsFile = Path.Combine(Aplicacao.Caminho, "Cenarios\\Arquivos\\excel-01.xls");
            this.docxFile = Path.Combine(Aplicacao.Caminho, "Cenarios\\Arquivos\\word-02.docx");
            this.pptxFile = Path.Combine(Aplicacao.Caminho, "Cenarios\\Arquivos\\powerpoint-01.pptx");

            Files.DeleteIfExists(this.docFilePdf);
            Files.DeleteIfExists(this.xlsFilePdf);
            Files.DeleteIfExists(this.docxFilePdf);
            Files.DeleteIfExists(this.pptxFilePdf);
        }

        [TestMethod]
        public void Deve_transformar_doc_em_pdf()
        {
            this.openOfficeTransformer.Execute(
                this.docFile,
                this.docFilePdf);

            Assert.IsTrue(File.Exists(this.docFilePdf));
        }

        [TestMethod]
        public void Deve_transformar_docx_em_pdf()
        {
            this.openOfficeTransformer.Execute(
                this.docxFile,
                this.docxFilePdf);

            Assert.IsTrue(File.Exists(this.docxFilePdf));
        }

        [TestMethod]
        public void Deve_transformar_xls_em_pdf()
        {
            this.openOfficeTransformer.Execute(
                this.xlsFile,
                this.xlsFilePdf);

            Assert.IsTrue(File.Exists(this.xlsFilePdf));
        }

        [TestMethod]
        public void Deve_transformar_pptx_em_pdf()
        {
            this.openOfficeTransformer.Execute(
                this.pptxFile,
                this.pptxFilePdf);

            Assert.IsTrue(File.Exists(this.pptxFilePdf));
        }
    }
}
