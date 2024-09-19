namespace Api.Models
{
    public class UploadedFile
    {
        public IFormFile file { get; }
        public string name { get; }
        public List<String> formats { get; }
        public bool isPublic { get; }

        public UploadedFile(FileBuilder builder)
        {
            this.file = builder.file;
            this.name = builder.name;
            this.formats = builder.formats;
            this.isPublic = builder.isPublic;
        }

        public class FileBuilder()
        {
            public IFormFile file { get; private set; }
            public string name { get; private set; }
            public List<String> formats { get; private set; } = new List<string>();
            public bool isPublic { get; private set; } = true;

            public FileBuilder File(IFormFile file)
            {
                this.file = file;
                return this;
            }
            public FileBuilder Name(string name)
            {
                this.name = name;
                return this;
            }
            public FileBuilder AllowImg()
            {
                this.formats.Add(".jpg");
                this.formats.Add(".jpeg");
                this.formats.Add(".png");
                return this;
            }
            public FileBuilder AllowDoc()
            {
                this.formats.Add(".pdf");
                return this;
            }
            public FileBuilder MakePrivate()
            {
                this.isPublic = false;
                return this;
            }
            public UploadedFile Build()
            {
                return new UploadedFile(this);
            }
        }


    }
}
