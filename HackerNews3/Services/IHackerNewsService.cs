using HackerNews3.Models;
using System.Threading.Tasks;

namespace HackerNews3.Services
{
    public interface IHackerNewsService
    {
        void SetHNBaseUrl(string hnUrl);
        Task<StoryCollection> GetBestStoriesAsync();
        Task<int[]> GetBestStoryIdsAsync();

        Task<StoryCollection> GetBestStoriesAsync(int[] storyIds);

    }
}
