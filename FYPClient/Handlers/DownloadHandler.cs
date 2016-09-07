using FYPClient.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FYPClient.Handlers
{
    class DownloadHandler
    {
        string fileObj;

        public string DownloadFile(string fileName)
        {
            Console.WriteLine("Download started....");
            string s3Bucket = ConfigurationManager.AppSettings["AwsS3"];
            string localStoragePath = ConfigurationManager.AppSettings["LocalStorage"] + fileName + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(s3Bucket + fileName,
                                        localStoragePath);
                }

            } catch(Exception e)
            {
                Debug.WriteLine("No file exists to download");
                return null;

            }
            return localStoragePath;
        }
    }
}
