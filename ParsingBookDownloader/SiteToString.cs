using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParsingBookDownloader
{
    class SiteToString
    {
        public string GetSite(string Url)
        {
            WebRequest request = WebRequest.Create(Url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
