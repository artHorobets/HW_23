using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Net;

namespace TestProject2
{
    public class UnitTest
    {
        [Fact]
        public void SendApiRequestAndGetCookies()
        {
            var body = new Dictionary<string, string>
        {
            { "ulogin", "art1613122" },
            { "upassword", "505558545" }
        };

            var headers = new Dictionary<string, string>
        {
            {"Content-Type", "application/x-www-form-urlencoded"}
        };

            var response = APIHelper.SendApiRequest(body, headers, "https://my.soyuz.in.ua/", Method.POST);
            var cookie = APIHelper.ExtractCookie(response, "zbs_lang");
            var cookie2 = APIHelper.ExtractCookie(response, "ulogin");
            var cookie3 = APIHelper.ExtractCookie(response, "upassword");

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://my.soyuz.in.ua");
            System.Threading.Thread.Sleep(3000);

            driver.Manage().Cookies.AddCookie(cookie);
            driver.Manage().Cookies.AddCookie(cookie2);
            driver.Manage().Cookies.AddCookie(cookie3);

            driver.Navigate().GoToUrl("https://my.soyuz.in.ua/index.php");

            System.Threading.Thread.Sleep(15000);
            driver.Close();
        }

        [Fact]
        public void DownloadImage()
        {
            WebClient client = new WebClient();
            client.DownloadFile("https://test.soyuz.in.ua/images/logo.png", @"C:/Users/Anna/Desktop/test.jpeg");
        }

        //[Fact]
        //public void UploadImage()
        //{
        //    IWebDriver driver = new ChromeDriver();
        //    driver.Navigate().GoToUrl("https://imgbb.com/");
        //    var client = new RestClient("https://imgbb.com");
        //    var request = new RestRequest(Method.POST);
        //    request.RequestFormat = DataFormat.Json;
        //    request.AddHeader("Content-Type", "multipart/form-data");
        //    request.AddFile("content", "C:/Users/Anna/Desktop/test.jpeg");
        //    IRestResponse response = client.Execute(request);
        //}
    }
}
