namespace Salus.Model.UI
{

    using System;

    public class FileViewModel
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
        public string Subject { get; set; }
    }
}
