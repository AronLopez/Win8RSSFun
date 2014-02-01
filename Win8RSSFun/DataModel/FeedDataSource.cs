using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace Win8RSSFun.DataModel
{
    public class FeedDataSource
    {
        private ObservableCollection<FeedData> _feeds = new ObservableCollection<FeedData>();
        public ObservableCollection<FeedData> Feeds
        {
            get
            {
                return _feeds;
            }
        }

        public async Task GetFeedsAsync()
        {
            Task<FeedData> feed1 = GetFeedAsync("http://windowsteamblog.com/windows/b/developers/atom.aspx");
            this.Feeds.Add(await feed1);
        }

        private async Task<FeedData> GetFeedAsync(string feedString)
        {
            Windows.Web.Syndication.SyndicationClient client = new Windows.Web.Syndication.SyndicationClient();
            Uri feedUri = new Uri(feedString);
            try
            {
                SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri);
                FeedData feedData = new FeedData();

                if (feed.Title != null && feed.Title.Text != null)
                {
                    feedData.Title = feed.Title.Text;
                }
                if (feed.Subtitle != null && feed.Subtitle.Text != null)
                {
                    feedData.Description = feed.Subtitle.Text;
                }

                if (feed.Items != null && feed.Items.Count > 0)
                {
                    foreach (SyndicationItem item in feed.Items)
                    {
                        FeedItem feedItem = new FeedItem();
                        if (item.Title != null && item.Title.Text != null)
                        {
                            feedItem.Title = item.Title.Text;
                        }
                        if (item.PublishedDate != null)
                        {
                            feedItem.PubDate = item.PublishedDate.DateTime;
                        }
                        if (item.Authors != null && item.Authors.Count > 0)
                        {
                            feedItem.Author = item.Authors[0].Name.ToString();
                        }
                        
                        if (feed.SourceFormat == SyndicationFormat.Atom10)
                        {
                            if (item.Content != null && item.Content.Text != null)
                            {
                                feedItem.Content = item.Content.Text;
                            }
                            if (item.Id != null)
                            {
                                feedItem.Link = item.Links[0].Uri;//new Uri("http://windowsteamblog.com" + item.Id);
                            }
                        }
                        else if (feed.SourceFormat == SyndicationFormat.Rss20)
                        {
                            if (item.Summary != null && item.Summary.Text != null)
                            {
                                feedItem.Content = item.Summary.Text;
                            }
                            if (item.Links != null && item.Links.Count > 0)
                            {
                                feedItem.Link = item.Links[0].Uri;
                            }
                        }
                        feedData.Items.Add(feedItem);
                    }
                }
                return feedData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static FeedData GetFeed(string title)
        {
            var _feedDataSource = App.Current.Resources["feedDataSource"] as FeedDataSource;

            var matches = _feedDataSource.Feeds.Where((feed) => feed.Title.Equals(title));
            if (matches.Count() == 1)
                return matches.First();
            return null;
        }

        public static FeedItem GetItem(string uniqueId)
        {
            var _feedDataSource = App.Current.Resources["feedDataSource"] as FeedDataSource;

            var matches = _feedDataSource.Feeds.SelectMany(group => group.Items).Where((item) => item.Title.Equals(uniqueId));
            if (matches.Count() == 1)
                return matches.First();
            return null;
        }
    }
}
