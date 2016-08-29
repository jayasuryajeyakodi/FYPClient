using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPClient.Models
{
    class FileObject
    {
        public string fileName { get; set; }
        public string localPath { get; set; }
        public string globalPath { get; set; }
        public string timeStamp { get; set; }
        public bool isUploadSuccess { get; set; }
    }
}
