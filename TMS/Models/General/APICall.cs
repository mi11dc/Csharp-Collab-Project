using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace TMS.Models.General
{
    public class APICall
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static APICall()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44371/api/");
        }

        public HttpResponseMessage Get(string url)
        {
            return client.GetAsync(url).Result;
        }

        public HttpResponseMessage Post(string url, Object obj)
        {
            string jsonpayload = jss.Serialize(obj);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            return client.PostAsync(url, content).Result;
        }
    }
}