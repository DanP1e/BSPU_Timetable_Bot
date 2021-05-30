using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace TimetableBot
{
    public class PageParser
    {
        public PageParser(string pageAdress)
        {
            PageAdress = pageAdress;
        }
        public string PageAdress { get; private set; }
        public HtmlDocument GetPageHTML()
        {        
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(PageAdress);
            return htmlDoc;           
        }
        
    }
}
