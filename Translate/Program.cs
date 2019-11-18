using System;
using System.Collections.Generic;
using System.IO;
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
            ReadTxtFile("list.txt");
            //TranslatedWord translatedWord = Request("administration");
            //Console.WriteLine(translatedWord.English + " => " +  string.Join(", ", translatedWord.Turkish));
            Console.ReadLine();
        }

        public static List<string> ReadTxtFile(string filePath)
        {
            List<string> words = new List<string>();
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                words.Add(line);
            }

            return words;
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
            TranslatedWord word = new TranslatedWord
            {
                English = textToConvert,
                Turkish = responseData.Text
            };

            return word;
        }
    }
}
