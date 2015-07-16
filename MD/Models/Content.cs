using MarkdownSharp;
using System;
using System.IO;

namespace MD.Models
{
    public class Content
    {
        public string Heading { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public string Markdown
        {
            get
            {
                return new Markdown().Transform(Body);
            }
        }

        public Content(string content)
        {
            string[] contentParts = content.Split('¤');
            if (contentParts.Length > 1)
            {
                Heading = contentParts[0];
                Body = contentParts[1];
            }
            else
                Body = contentParts[0];
        }

        public Content(string fileName, string fileContent, string blogName)
        {
            string[] fileNameParts = fileName.Split('_');
            Date = DateTime.Parse(fileNameParts[0]);
            string[] contentParts = fileContent.Split('¤');
            if (contentParts.Length > 1)
            {
                Heading = contentParts[0];
                Body = contentParts[1];
            }
            else
                Body = contentParts[0];

            Url = string.Format(@"/{0}/{1}", blogName, fileName);
        }
    }

}


