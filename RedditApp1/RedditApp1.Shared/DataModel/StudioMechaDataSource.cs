using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Notifications;

namespace RedditApp1.DataModel
{
 
    public sealed class StudioMechaDataSource
    {

        private static StudioMechaDataSource _studioMechaDataSource = new StudioMechaDataSource();

        public static async Task<IEnumerable<StudioMechaDataNewsItem>> GetAsync()
        {
            await _studioMechaDataSource.GetDataAsync();
            return _studioMechaDataSource.News;
        }

        private ObservableCollection<StudioMechaDataNewsItem> _news = new ObservableCollection<StudioMechaDataNewsItem>();

        public ObservableCollection<StudioMechaDataNewsItem> News
        {
            get { return _news; }
        }

      
        private async Task GetDataAsync()
        {
            if (this._news.Count != 0)
                return;

            Uri dataUri = new Uri("ms-appx:///DataModel/StudiomechaData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["News"].GetArray();

            foreach (JsonValue newsValue in jsonArray)
            {
                JsonObject newsObject = newsValue.GetObject();
                this.News.Add(new StudioMechaDataNewsItem(newsObject));
            }
           
        }


        ////Reddit data
        //private ObservableCollection<RedditDataItem> _redditItems = new ObservableCollection<RedditDataItem>();
        //public ObservableCollection<RedditDataItem> Reddit
        //{
        //    get { return this._redditItems; }
        //}

        //public static async Task<IEnumerable<RedditDataItem>> GetAsync()
        //{
        //    await _studioMechaDataSource.GetRedditDataAsync();

        //    return _studioMechaDataSource.Reddit;
        //}



        //private async Task GetRedditDataAsync()
        //{

        //    Uri dataUri = new Uri("ms-appx:///DataModel/RedditData.json");

        //    StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
        //    string jsonText = await FileIO.ReadTextAsync(file);
        //    JsonObject jsonObject = JsonObject.Parse(jsonText);
        //    JsonArray itemArray = jsonObject["data"].GetObject()["children"].GetArray();

        //    foreach (JsonValue groupValue in itemArray)
        //    {
        //        RedditDataItem rdi = new RedditDataItem(groupValue.GetObject());
        //        this.Reddit.Add(rdi);
        //    }
        //}
    }
}
