using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Win8RSSFun.DataModel
{
    public class FeedItem
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        private DateTime pubDate;

        public DateTime PubDate
        {
            get { return pubDate; }
            set { pubDate = value; }
        }

        private Uri link;

        public Uri Link
        {
            get { return link; }
            set { link = value; }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

    }
}
