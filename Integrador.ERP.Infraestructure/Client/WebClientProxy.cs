using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.ERP.Infraestructure.Client
{
    public static class WebClientProxy
    {
        public const string MediaTypeJson = "Application/JSON; charset=utf-8";

        public static WebClient GetWebClient()
        {
            MyWebClient webClient = new MyWebClient();
            webClient.Headers.Add("Content-Type", MediaTypeJson);
            webClient.Headers.Add("Accept-Type", MediaTypeJson);
            webClient.Encoding = System.Text.Encoding.UTF8;
            return webClient;
        }

        public static HttpClient GetWebClientHttp()
        {
            HttpClient webClient = new HttpClient();
            webClient.DefaultRequestHeaders.Add("Content-Type", MediaTypeJson);
            webClient.DefaultRequestHeaders.Add("Accept-Type", MediaTypeJson);
            webClient.Timeout = new TimeSpan(0,0, 1200000);
            return webClient;
        }

    }

    public class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest w = base.GetWebRequest(address);
            w.Timeout = 1200000;

            return w;
        }
    }
}
