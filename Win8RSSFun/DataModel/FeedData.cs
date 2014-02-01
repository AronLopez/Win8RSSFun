using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win8RSSFun.DataModel
{
    public class FeedData
    {
        private String tittle;

        public String Title
        {
            get { return tittle; }
            set { tittle = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private DateTime pubDate;

        public DateTime PubDate
        {
            get { return pubDate; }
            set { pubDate = value; }
        }

        private List<FeedItem> items = new List<FeedItem>();

        public List<FeedItem> Items
        {
            get { return items; }
            set { items = value; }
        }
    }
}
