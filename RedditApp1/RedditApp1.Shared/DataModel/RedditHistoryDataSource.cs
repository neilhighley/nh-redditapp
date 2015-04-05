using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace RedditApp1.DataModel
{
    
    /// <summary>
    /// Historical datasource, from local storage
    /// </summary>
    public sealed class RedditHistoryDataSource
   {
      // private static string _redditURI = "http://www.reddit.com/hot.json";

        private static RedditHistoryDataSource _redditDataSource = new RedditHistoryDataSource();

       private ObservableCollection<RedditDataItem> _items = new ObservableCollection<RedditDataItem>();
       public ObservableCollection<RedditDataItem> Items
        {
            get { return this._items; }
        }

       public static async Task<IEnumerable<RedditDataItem>> GetAsync()
        {
            await _redditDataSource.GetRedditDataAsync();

            return _redditDataSource.Items;
        }


       //old version
       //private async Task GetRedditDataAsync()
       // {

       //     Uri dataUri = new Uri("ms-appx:///DataModel/RedditData.json");

       //     StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
       //     string jsonText = await FileIO.ReadTextAsync(file);
       //     JsonObject jsonObject = JsonObject.Parse(jsonText);
       //     JsonArray itemArray = jsonObject["data"].GetObject()["Groups"].GetArray();

       //     foreach (JsonValue groupValue in itemArray)
       //     {
       //         RedditDataItem rdi=new RedditDataItem(groupValue.GetObject());
       //        this.Items.Add(rdi);
       //     }
       // }

       private async Task GetRedditDataAsync()
       {

           Uri dataUri = new Uri("ms-appx:///DataModel/RedditData.json");

           StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
           string jsonText = await FileIO.ReadTextAsync(file);
           JsonObject jsonObject = JsonObject.Parse(jsonText);
           JsonArray itemArray = jsonObject["data"].GetObject()["children"].GetArray();

           foreach (JsonValue groupValue in itemArray)
           {
               RedditDataItem rdi = new RedditDataItem(groupValue.GetObject());
               this.Items.Add(rdi);
           }
       }
    }
}
