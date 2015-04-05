using System;
using System.Collections.Generic;
using System.Text;
using Windows.Data.Json;

namespace RedditApp1.DataModel
{
    public class StudioMechaDataNewsItem
    {
        public StudioMechaDataNewsItem(JsonObject itemObject)
        {
            Title = itemObject["Title"].GetString();
            SubHeading = itemObject["SubHeading"].GetString();
            Url = itemObject["Url"].GetString();
        }

        public String Title { get; set; }
        public String SubHeading { get; set; }
        public String Url { get; set; }

    }
    public class RedditDataItem
    {
        public RedditDataItem(JsonObject itemObject)
        {

            var dataObject = itemObject["data"].GetObject();

            Title = dataObject["title"].GetString();
            Permalink = dataObject["permalink"].GetString();
            Score = dataObject["score"].GetNumber();
            Thumbnail = dataObject["thumbnail"].GetString();
            Over18 = dataObject["over_18"].GetBoolean();
            UpVotes = dataObject["ups"].GetNumber();
            DownVotes = dataObject["downs"].GetNumber();
            Url = dataObject["url"].GetString();
        }

        public bool Over18 { get; set; }
        public String Title { get; set; }
        public String SubHeading { get; set; }
        public String Url { get; set; }
        public double UpVotes { get; set; }
        public double DownVotes { get; set; }
        public string SubReddit { get; set; }
        public string Thumbnail { get; set; }
        public string Author { get; set; }
        public string Permalink { get; set; }
        public string Name { get; set; }
        public double Created { get; set; }
        public double CreatedUTC { get; set; }
        public double Score { get; set; }

    }
}
