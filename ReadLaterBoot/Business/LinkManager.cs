using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ReadLaterBot.Business
{
    public class LinkManager
    {
        public void SaveLink(string userID, string link)
        {
            SaveLinks(userID, new string[] { link });
        }

        public void RemoveLink(string userID, int linkIndex)
        {
            string[] links = GetLinks(userID);

            List<string> linksToKeep = new List<string>();

            for (int i = 0; i < links.Length; i++)
            {
                if (i != linkIndex)
                    linksToKeep.Add(links[i]);
            }

            ClearList(userID);

            SaveLinks(userID, linksToKeep.ToArray());
        }

        private void SaveLinks(string userID, string[] links)
        {
            using (StreamWriter sw = System.IO.File.AppendText(string.Format(@"C:\ReadLaterBot\{0}.txt", userID)))
            {
                foreach (string link in links)
                {
                    sw.WriteLine(link);
                    sw.Flush();
                }
            }
        }

        public string[] GetLinks(string userID)
        {
            List<string> links = new List<string>();

            string fileName = string.Format(@"C:\ReadLaterBot\{0}.txt", userID);

            if (System.IO.File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string link = string.Empty;

                        do
                        {
                            link = sr.ReadLine();

                            if (!string.IsNullOrEmpty(link))
                                links.Add(link);

                        } while (!string.IsNullOrEmpty(link));
                    }
                }
            }

            return links.ToArray();
        }

        public void ClearList(string userID)
        {
            System.IO.File.Delete(string.Format(@"C:\ReadLaterBot\{0}.txt", userID));
        }
    }
}
