using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HackerNews3.Models;
using HackerNews3.Repository;
using System.Configuration;
using HackerNews3.Services;
using System.Threading.Tasks;

namespace HackerNews3.Controllers
{
    public class HomeController : Controller
    {
        private IHackerNewsService _hackerNewsService;

        private string _hnBaseUrl = null;
        public string HnBaseUrl
        {
            get
            {
                if (_hnBaseUrl == null)
                {
                    _hnBaseUrl = ConfigurationManager.AppSettings["HNBaseUrl"];
                }
                return _hnBaseUrl;
            }
            set { _hnBaseUrl = value;   }
        }

        private int _pageSize = 0;
        public int PageSize
        {
            get
            {
                if (_pageSize == 0)
                {
                    _pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
                }
                return _pageSize;
            }
            set { _pageSize = value;  }
        }


        public HomeController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [OutputCache(Duration = 600, VaryByParam = "page")]
        public async Task<ActionResult> Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            //string hnBaseUrl = ConfigurationManager.AppSettings["HNBaseUrl"];

            //int pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());

            _hackerNewsService.SetHNBaseUrl(HnBaseUrl);
            StoryRepo storyRepo = new StoryRepo(_hackerNewsService, HnBaseUrl);

            int[] storyIds = await storyRepo.GetBestStoryIds();

            ViewBag.NumOfPages = Math.Ceiling((double)storyIds.Length / PageSize);
            
            ViewBag.CurrentPageNumber = page;

            int[] currentStoryIds = storyIds
                                        .Skip((page.Value - 1) * PageSize)
                                        .Take(PageSize)
                                        .ToArray();           
            
            StoryCollection colStory = await storyRepo.GetBestStories(currentStoryIds);

            return View(colStory);
        }
    }
}