using FYPClient.Models;
using FYPClient.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPClient.Handlers
{
    class FileTagHandler
    {
        FileObject fileObject;
        public FileTagHandler(FileObject _fileObject)
        {
            fileObject = _fileObject;
        }
        public string GetFileTag()
        {
            CallApi callApi = new CallApi(ConfigurationManager.AppSettings["FileTagGeneratorApi"] + fileObject.fileName);
            string jsonResponse = callApi.getJson() ;
            dynamic data = JObject.Parse(jsonResponse);
            string fileTag = data.fileTag;
            Debug.WriteLine("File Tag is " + fileTag );
            return fileTag;
        }
    }
}
