using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Globalization;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using HtmlAgilityPack;
using AutoStockBroker.Models;
using System.Web.Hosting;

namespace AutoStockBroker.Models
{
    public static class JsonCreator
    {
        public static List<Stock> AddJSON(List<Stock> stocks)
        {
            using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("~/Root/Json/AutoSTockBrokerJSON.json")))
            {

                //JsonConvert.DeserializeObject<string>(sr.ReadToEnd());
                string autoStockBrokerJson = sr.ReadToEnd();
                JObject autoStockBrokerJobject = JObject.Parse(autoStockBrokerJson);

                JToken jStocks = autoStockBrokerJobject["stocks"];
                foreach (var stock in jStocks)
                {
                    stocks.Add(new Stock()
                    {
                        Name = stock["name"].Value<string>(),

                    });
                }
                return stocks;
                //JObject results = JsonConvert.DeserializeObject<JObject>(autoStockBrokerJson);

                //string autoStockBrokerJsonFormatted = autoStockBrokerJson.ToString(Formatting.None);
                //JObject autoStockBrokerJobject = JObject.Parse(autoStockBrokerJsonFormatted);

                //var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                //var content = JObject.Parse(JsonConvert.SerializeObject(request, settings));
            }
        }

        public static string CreateJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static void SaveToRoot(string json)
        {
            File.WriteAllText(HostingEnvironment.ApplicationPhysicalPath + @"Root\Json\stocks.json", json);
        }
    }
}