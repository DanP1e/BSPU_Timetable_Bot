using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TimetableBot.Parser
{
    public class FileDownloader
    {
        private string fileName;
        private string adress;

        public FileDownloader(string fileName, string adress)
        {
            this.fileName = fileName;
            this.adress = adress;
        }

        public void Download() 
        {
            using (WebClient wc = new WebClient())
            {              
                wc.DownloadFile(adress, fileName);
                if (wc == null)
                {
                    throw new ArgumentException(nameof(adress), "Invalid file address! No such file exists.");
                }
            }
        }
    }
}
