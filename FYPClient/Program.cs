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
            bool isContinued = true;
            do
            {
                Console.WriteLine(Strings.menu);

                int responseCode = Int32.Parse(Console.ReadLine());
                FileObject fileObj = new FileObject();
                switch (responseCode)
                {
                    case 1:
                        FileHandler fileHandler = new FileHandler();
                        fileObj = fileHandler.init();
                        FileTagHandler fileTagHandler = new FileTagHandler(fileObj);
                        string fileTag = fileTagHandler.GetFileTag();
                        if (fileTag == null || fileTag == "")
                        {
                            Console.WriteLine("File Does not exist");
                            break;
                        }
                        Debug.WriteLine("Inside main: file tag is " + fileTag);
                        Console.WriteLine("File Tag generation Successfull : <File Tag>" + fileTag);
                        break;
                    case 2:
                        FileUploadHandler fileUploadHandler = new FileUploadHandler(fileObj);
                        string globalPath = fileUploadHandler.UploadFile();
                        if(globalPath == null || globalPath == "")
                        {
                            Console.WriteLine("Upload Failed");
                            break;
                        }
                        fileObj.globalPath = globalPath;
                        Console.WriteLine("Uploaded Successfully " + fileObj.globalPath);
                        break;
                    case 3:
                        Console.WriteLine("Enter the name of the file");
                        string fileName = Console.ReadLine();
                        string downloadedFilePath = new DownloadHandler().DownloadFile(fileName);
                        if (downloadedFilePath == null || downloadedFilePath == "")
                        {
                            Console.WriteLine("Download failed! No file exists");
                        }
                        else
                        {
                            Console.WriteLine("Download Successfull, stored in " + downloadedFilePath);
                        }
                        break;
                }
                Console.WriteLine("Do you want to continue? \n 1.Yes 2.No");
                string answer = Console.ReadLine();
                if (answer == "2") isContinued = false;
            } while (isContinued);
            
        }
    }
}
