namespace Salus.Infra.IO
{
    using System.IO;

    public class Files
    {
        public static void DeleteIfExists(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}
