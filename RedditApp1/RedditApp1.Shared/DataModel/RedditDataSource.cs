using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using RedditApp1.Data;

namespace RedditApp1.DataModel
{
    public class RedditDataItem {

        public string UniqueId;
    }

    public class RedditDataGroup
    {
        public List<RedditDataItem> Items;
        public string UniqueId;
    }


    public sealed class RedditDataSource
   {
       private static string _redditURI = "http://www.reddit.com/hot.json";

       private static RedditDataSource _redditDataSource = new RedditDataSource();

       private ObservableCollection<RedditDataGroup> _groups = new ObservableCollection<RedditDataGroup>();
       public ObservableCollection<RedditDataGroup> Groups
        {
            get { return this._groups; }
        }

       public static async Task<IEnumerable<RedditDataGroup>> GetGroupsAsync()
        {
            await _redditDataSource.GetSampleDataAsync();

            return _redditDataSource.Groups;
        }

       public static async Task<RedditDataGroup> GetGroupAsync(string uniqueId)
        {
            await _redditDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _redditDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<RedditDataItem> GetItemAsync(string uniqueId)
        {
            await _redditDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _redditDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetSampleDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            Uri dataUri = new Uri("ms-appx:///DataModel/SampleData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Groups"].GetArray();

            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                RedditDataGroup group = new RedditDataGroup();

                foreach (JsonValue itemValue in groupObject["Items"].GetArray())
                {
                    JsonObject itemObject = itemValue.GetObject();
                    group.Items.Add(new RedditDataItem());
                }
                this.Groups.Add(group);
            }
        }
    }
}
