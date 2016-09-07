using FYPClient.Models;
using FYPClient.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPClient.Handlers
{
    class FileUploadHandler
    {
        FileObject fileObject;
        public FileUploadHandler(FileObject _fileObject)
        {
            fileObject = _fileObject;
        }

        public string UploadFile()
        {
            string accessKey = ConfigurationManager.AppSettings["AwsAccessKey"];
            string secretAccessKey = ConfigurationManager.AppSettings["AwsSecretKey"];
            string s3Bucket = ConfigurationManager.AppSettings["AwsBucketName"];
            string serviceUrl = ConfigurationManager.AppSettings["AwsServiceUrl"];  //Oregon service url 

            Console.WriteLine("Enter file path to upload");
            string localPath = Console.ReadLine();
            string filePath = localPath; // "C:\\Users\\Jaya\\Documents\\" + ;
            if (File.Exists(filePath) == false)
            {
                Console.WriteLine("File does not exist");
                return null;
            }
            Debug.WriteLine("Uploading file " + localPath);
            string newFileName = "CloudFile";// + DateTime.Now.Ticks.ToString(); //new filename in s3, optional

            Console.WriteLine("Enter the new file name to store in the cloud");
            newFileName = Console.ReadLine();

            /*********************************/
            Console.WriteLine("Encrypting using AES-256 ......");
            AesCrypto aes = new AesCrypto();
            filePath = aes.Encrypt(localPath);
            Console.WriteLine("AES-256 Encryption done successfully....");
            /*********************************/

            Stopwatch stopWatch = new Stopwatch();

            //create aws object
            S3 s3 = new S3(accessKey, secretAccessKey, serviceUrl);

            Console.WriteLine("Uploading....");
            stopWatch.Start();
            //upload
            s3.UploadFile(filePath, s3Bucket, newFileName, false);
            Console.WriteLine("Upload Successfull!");
            stopWatch.Stop();

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("UploadTime " + elapsedTime);
            Debug.WriteLine("UploadTime " + elapsedTime);
            fileObject.globalPath = ConfigurationManager.AppSettings["AwsS3"] + newFileName;
            return fileObject.globalPath;
        }
    }
}
