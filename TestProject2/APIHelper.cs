using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using OpenQA.Selenium;
using System.IO;

namespace TestProject2
{
    public static class APIHelper
    {
        public static IRestResponse SendApiRequest(Dictionary<string, string> body, Dictionary<string, string> headers, string link, Method type)
        {
            RestClient client = new RestClient(link)
            {
                Timeout = 300000
            };
            RestRequest request = new RestRequest(type);
            foreach (var header in headers)
                request.AddHeader(header.Key, header.Value);

            foreach (var data in body)
                request.AddParameter(data.Key, data.Value);

            IRestResponse response = client.Execute(request);
            return response;
        }

        public static Cookie ExtractCookie(IRestResponse response, string cookieName)
        {
            Cookie result = null;
            foreach (var cookie in response.Cookies)
                if (cookie.Name.Equals(cookieName))
                    result = new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null);
            return result;
        }

        public static bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

    }
}