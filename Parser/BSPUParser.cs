using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;
namespace TimetableBot
{
    public class BSPUParser : PageParser
    {

        public const string ffmkto = "http://bdpu.org/timetable-ffmkto/";
        public BSPUParser() : base (ffmkto)
        { }
        public string GetTimetableLink(HtmlNode timetable) 
        {
            if (timetable == null)
            {
                throw new System.ArgumentNullException();
            }
            string[] htmlSplit = timetable.InnerHtml.Split('\"');

            if (htmlSplit.Length == 3)
            {
                return htmlSplit[1];
            }
            return "";
            
            
        }
        
        public HtmlNode GetActualTimetable(string groupTag)
        {
            HtmlNode[] nodes = GetTimetablesParentNode().ChildNodes.Where(n => n.InnerText.Contains("Денна")).ToArray();
            HtmlNode dtTimetable = null;
            DateTime dateTime = DateTime.MinValue;
            if (nodes.Length != 0)
            {              
               
                foreach (var i in nodes)
                {
                    string[] textSplit = i.InnerText.Split(" ");
                    try
                    {
                        if (textSplit.Length == 3 && textSplit[1] == groupTag)
                        {
                            string[] dateSplit = textSplit[2].Split('.');
                            int days = Convert.ToInt32(dateSplit[0]);
                            int month = Convert.ToInt32(dateSplit[1]);
                            int years = Convert.ToInt32(dateSplit[2]);

                            DateTime tableDate = new DateTime(years, month, days);

                            if (tableDate > dateTime)
                            {
                                dateTime = tableDate;
                                dtTimetable = i;
                            }

                        }
                    }
                    catch
                    {
                        continue;
                    }
                }         
                
            }
            else
            {
               Logger.Error($"Page haven't {groupTag} tagged timetable");               
            }
            return dtTimetable;
        }
        private HtmlNode GetTimetablesParentNode()
        {            
            HtmlDocument doc = GetPageHTML();
            IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants().Where(n => n.HasClass("general__style"));
            if (nodes.Count() == 0) return null;
            return nodes.ElementAt(0);
        }
    }
}
