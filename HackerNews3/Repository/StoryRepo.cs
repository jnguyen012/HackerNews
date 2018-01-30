using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HackerNews3.Services;
using HackerNews3.Models;
using System.Threading.Tasks;


namespace HackerNews3.Repository
{
    public class StoryRepo
    {
        private IHackerNewsService _hackerNewsService;
        public string HNBaseUrl  { set; get; }

        public StoryRepo(IHackerNewsService hackerNewsService, string hnBaseUrl)
        {
            _hackerNewsService = hackerNewsService;
            HNBaseUrl = hnBaseUrl;
        }

        public StoryRepo(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        public StoryRepo(string hnBaseUrl)
        {
            HNBaseUrl = hnBaseUrl;
        }
        public async Task<StoryCollection> GetBestStories()
        {
            StoryCollection colStory = null;

            colStory = await _hackerNewsService.GetBestStoriesAsync();

            return colStory;
        }

        public async Task<StoryCollection> GetBestStories(int[] storyIds)
        {
            StoryCollection colStory = null;

            colStory = await _hackerNewsService.GetBestStoriesAsync(storyIds);

            return colStory;
        }

        public async Task<int[]> GetBestStoryIds()
        {
            int[] bestStoryIds;
            bestStoryIds = await _hackerNewsService.GetBestStoryIdsAsync();

            return bestStoryIds;
        }

        
    }
}