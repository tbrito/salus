namespace SalusCmd.Ecm6
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.FtpClient;

    public class Ftp2
    {
        private readonly string ftpUser;
        private readonly string rootPath;
        private readonly FtpClient ftpClient;

        public Ftp2(string host, int port, string user, string password, string rootPath)
        {
            this.ftpUser = user;
            this.rootPath = rootPath;

            this.ftpClient = new FtpClient
            {
                Host = host,
                Port = port,
                Credentials = new NetworkCredential(user, password),
            };
        }

        public string Root
        {
            get
            {
                return this.rootPath;
            }
        }

        public IEnumerable<string> GetDirectories(string directory)
        {
            foreach (FtpListItem item in this.ftpClient.GetListing(
                directory,
                FtpListOption.Modify | FtpListOption.Size))
            {
                if (item.Type == FtpFileSystemObjectType.Directory)
                {
                    yield return item.FullName;
                }
            }
        }

        public IEnumerable<string> GetFiles(string directory)
        {
            foreach (FtpListItem item in this.ftpClient.GetListing(
                directory,
                FtpListOption.Modify | FtpListOption.Size))
            {
                if (item.Type == FtpFileSystemObjectType.File)
                {
                    yield return item.FullName;
                }
            }
        }

        public void DownloadFile(string source, string destination)
        {
            this.Download(source, destination);
        }

        public void Connect()
        {
            this.ftpClient.Connect();
            this.ftpClient.SetWorkingDirectory(this.rootPath);
        }

        private void Download(string remoteFilePath, string localFilePath)
        {
            using (var fs = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // istream.Position is incremented accordingly to the reads you perform
                // istream.Length == file size if the server supports getting the file size
                // also note that file size for the same file can vary between ASCII and Binary
                // modes and some servers won't even give a file size for ASCII files! It is
                // recommended that you stick with Binary and worry about character encodings
                // on your end of the connection.
                var buffer = new byte[8192];
                var offset = 0;
                var remoteStream = this.ftpClient.OpenRead(remoteFilePath);
                try
                {
                    while (offset < remoteStream.Length)
                    {
                        try
                        {
                            int len;
                            while ((len = remoteStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fs.Write(buffer, 0, len);
                                offset += len;
                            }
                        }
                        catch (IOException ex)
                        {
                            if (ex.InnerException != null)
                            {
                                var iex = ex.InnerException as System.Net.Sockets.SocketException;
                                if (iex != null && iex.ErrorCode == 10054)
                                {
                                    remoteStream.Close();
                                    remoteStream = this.ftpClient.OpenRead(remoteFilePath, offset);
                                }
                                else
                                {
                                    throw;
                                }
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
                finally
                {
                    remoteStream.Close();
                }
            }
        }
    }
}
