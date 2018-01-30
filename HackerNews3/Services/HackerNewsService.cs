using System;
using HackerNews3.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


namespace HackerNews3.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        public string HNBaseUrl { set; get; }

        public HackerNewsService()
        { }

        public void SetHNBaseUrl(string hnUrl)
        {
            HNBaseUrl = hnUrl;
        }

        public async Task<StoryCollection> GetBestStoriesAsync()
        {
            StoryCollection colStory = new StoryCollection();

            Uri uri = new Uri(HNBaseUrl + "/beststories.json");

            RequestResult requestResult = await HttpHelper.SendRequestAsync(uri);

            if (requestResult.IsSuccess)
            {
                List<int> lstBestStoryIds = null;

                JArray rootObject = (JArray)JsonConvert.DeserializeObject(requestResult.Content);
                lstBestStoryIds = rootObject.ToObject<List<int>>();

                foreach (int storyId in lstBestStoryIds)
                {
                    Story story = await GetStoryAsync(storyId);

                    if (story != null)
                    {
                        colStory.Add(story);
                    }
                }

            }
            
            return colStory;
        }

        public async Task<StoryCollection> GetBestStoriesAsync(int[] storyIds)
        {
            StoryCollection colStory = new StoryCollection();

            Uri uri = new Uri(HNBaseUrl + "/beststories.json");
            for (var i=0; i < storyIds.Length; i++)
            {
                Story story = await GetStoryAsync(storyIds[i]);

                if (story != null)
                {
                    colStory.Add(story);
                }
            }

            return colStory;
        }

        public async Task<Story> GetStoryAsync(int id)
        {
            Story story = null;

            Uri uri = new Uri(HNBaseUrl + $"/item/{id}.json");
            RequestResult requestResult = await HttpHelper.SendRequestAsync(uri);

            if (requestResult.IsSuccess)
            {
                JObject rootObject = (JObject) JsonConvert.DeserializeObject(requestResult.Content);

                story = rootObject.ToObject<Story>();
            }

            return story;
        }

        public async Task<int[]> GetBestStoryIdsAsync()
        {
            List<int> lstBestStoryIds = null;
            StoryCollection colStory = new StoryCollection();

            Uri uri = new Uri(HNBaseUrl + "/beststories.json");

            RequestResult requestResult = await HttpHelper.SendRequestAsync(uri);

            if (requestResult.IsSuccess)
            {


                JArray rootObject = (JArray) JsonConvert.DeserializeObject(requestResult.Content);
                lstBestStoryIds = rootObject.ToObject<List<int>>();
            }

            return lstBestStoryIds.ToArray();
        }
    }
}