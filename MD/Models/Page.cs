using System.Collections.Generic;
using System.Linq;

namespace MD.Models
{
    public class Page
    {
        public string Heading
        {
            get
            {
                return Contents.First().Heading;
            }
        }
        public SiteType Type { get; set; }
        public List<Content> Contents { get; set; }
        public Page()
        {
            Contents = new List<Content>();
        }

        public Page(string content)
        {
            Contents = new List<Content>() { new Content(content)};
            Type = SiteType.SingelPage;
        }

        public List<Content> SortedContents
        {
            get
            {
                return Contents.OrderByDescending(t => t.Date).ToList();
            }
        }



    }
}