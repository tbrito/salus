namespace Salus.Infra.Transformations
{
    using Logs;
    using System;
    using System.Diagnostics;
    using System.IO;

    public class OpenOfficeTransformer
    {
        private readonly string openOfficePath;

        public OpenOfficeTransformer()
        {
            var programFilesPath = this.GetProgramFilesPath();
            this.openOfficePath = Path.Combine(programFilesPath, "OpenOffice 4");
        }

        public string OpenOfficePath
        {
            get
            {
                return this.openOfficePath;
            }
        }

        public void Execute(string source, string destination)
        {
            Log.App.DebugFormat("Transformando {0} em {1}", source, destination);

            var process = this.GetProcess(source, destination);

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException(
                    "Erro " + process.ExitCode + ": " + process.StandardOutput.ReadToEnd());
            }

            Log.App.DebugFormat("{0} foi criado", destination);
        }

        private Process GetProcess(string source, string destination)
        {
            string arguments = string.Format(
                "\"{0}\" \"{1}\" \"{2}\"",
                Path.Combine(this.openOfficePath, this.GetPhytonConverterScript()),
                source,
                destination);

            return this.CreateProcessInOpenOfficePath("program\\python.exe", arguments);
        }

        private string GetPhytonConverterScript()
        {
            return Path.Combine(this.openOfficePath, "program\\DocumentConverter.py");
        }

        private Process CreateProcessInOpenOfficePath(string filename, string arguments)
        {
            var processFileName = Path.Combine(this.openOfficePath, filename);

            Log.App.DebugFormat("Executando {0} {1}", processFileName, arguments);

            return new Process
            {
                StartInfo =
                {
                    FileName = processFileName,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
                }
            };
        }

        /// <summary>
        /// TODO: colocar no framework
        /// </summary>
        /// <returns></returns>
        private string GetProgramFilesPath()
        {
            if (8 == IntPtr.Size || (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")) == false))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
    }
}
