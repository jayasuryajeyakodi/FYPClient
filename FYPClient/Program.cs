using FYPClient.Handlers;
using FYPClient.Models;
using FYPClient.Resources;
using FYPClient.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPClient
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine(Strings.welcomeMessage);
            Console.WriteLine(Strings.menu);

            int responseCode = Int32.Parse(Console.ReadLine());
            FileObject fileObj = new FileObject();
            switch(responseCode)
            {
                case 1:
                    FileHandler fileHandler = new FileHandler();
                    fileObj = fileHandler.init();
                    FileTagHandler fileTagHandler = new FileTagHandler(fileObj);
                    string fileTag = fileTagHandler.GetFileTag();

                    Debug.WriteLine("Inside main: file tag is " + fileTag);
                    Console.WriteLine("File Tag generation Successfull : <File Tag>" + fileTag);
                    break;
                case 2:
                 
                    FileUploadHandler fileUploadHandler = new FileUploadHandler(fileObj);
                    fileObj.globalPath = fileUploadHandler.UploadFile().globalPath;
                    Console.WriteLine("Uploaded Successfully " + fileObj.globalPath);
                    break;
                case 3: Console.WriteLine("Downloaded Successfully");
                        break;
            }

            Console.ReadLine();
        }
    }
}
