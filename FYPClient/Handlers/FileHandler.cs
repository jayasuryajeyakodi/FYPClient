using FYPClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPClient.Handlers
{
    class FileHandler
    {
        public FileObject fileObj;
        public FileHandler()
        {
            fileObj = new FileObject();
        }

        public FileObject init()
        {
            Console.WriteLine("Enter the path of the file \n ");
            string filePath = Console.ReadLine();
            string fileName;
            Console.WriteLine("Enter a new name for the file");
            fileName = Console.ReadLine();
            fileObj.fileName = fileName;
            fileObj.localPath = filePath;
            return fileObj;
        }
    }
}
