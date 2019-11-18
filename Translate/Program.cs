using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Translate
{
    class Program
    {
        static void Main(string[] args)
        {
            TranslatedWord translatedWord = Request("administration");
            Console.WriteLine(translatedWord.English + " => " +  string.Join(", ", translatedWord.Turkish));
            Console.ReadLine();
        }

        public static TranslatedWord Request(string textToConvert)
        {
            var client = new RestClient("https://translate.yandex.net/api/v1.5/tr.json");
            var request = new RestRequest("/translate");
            request.AddParameter("lang", "en-tr");
            request.AddParameter("text", textToConvert);
            request.AddParameter("key", "trnsl.1.1.20191118T004713Z.a2faaa96b41ab88d.007291ac94c0d0a76888ab6ccf20b6a4a1ca0e1f");
            request.AddHeader("Content-type", "application/x-www-form-urlencoded");

            var response = client.Post(request);
            var content = response.Content;
            ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(content);
            //var response = client.Post<ResponseData>(request);
            TranslatedWord word = new TranslatedWord
            {
                English = textToConvert,
                Turkish = responseData.Text
            };

            return word;
        }
    }
}
