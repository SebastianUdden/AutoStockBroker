using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace AutoStockBroker.Models.HtmlParsers
{
    public static class MiscParsers
    {
        public static string ParseIndividualHTML(string website)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(website));
            HtmlNode root = html.DocumentNode;
            HtmlNode topbox = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("RightTopBoxBig")).Single();
            HtmlNode topBoxtable = topbox.Descendants("table").Single();
            string content = topBoxtable.Descendants("tr").ElementAt(1).Descendants("td").Single().InnerHtml;
            string stockValue = Regex.Match(content, @"\d+(\,\d{1,2})?").Value;

            return stockValue;

            #region Not used
            //var html = new HtmlDocument();
            //html.LoadHtml(new WebClient().DownloadString("http://forums.asp.net/members/Mikesdotnetting.aspx"));
            //var root = html.DocumentNode;
            //var p = root.Descendants()
            //    .Where(n => n.GetAttributeValue("class", "").Equals("module-profile-recognition"))
            //    .Single()
            //    .Descendants("p")
            //    .Single();
            //var content = p.InnerText;

            //HttpClient http = new HttpClient();
            //var response = await http.GetByteArrayAsync(website);
            //String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            //source = WebUtility.HtmlDecode(source);
            //HtmlDocument resultat = new HtmlDocument();
            //resultat.LoadHtml(source);

            //List<HtmlNode> toftitle = resultat.DocumentNode.Descendants().Where(
            //    x => (x.Name == "div" && x.Attributes["class"] != null &&
            //    x.Attributes["class"].Value.Contains("block_content"))).ToList();

            //return source;
            #endregion
        }
    }
}