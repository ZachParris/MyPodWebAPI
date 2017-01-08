using MyPodWebAPI.DAL;
using MyPodWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace MyPodWebAPI.Controllers
{
    public class PodcastController : ApiController
    {
        private MyPodRepo repo = null;

        public PodcastController()
        {
            repo = new MyPodRepo();
        }

        [Authorize]
        // GET: api/Search
        public List<Podcast> Get()
        {
            return repo.GetUsersPodcasts(GetCurrentUser());
        }

        private string GetCurrentUser()
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;
            return userName;
        }

        // GET: api/Search/5
        public Podcast Get(int id)
        {
            if(repo.GetUsersPodcasts(GetCurrentUser()).Any(p => p.PodcastId == id))
            {
                return repo.GetPodcastById(id);
            }
            return null;
        }

        // POST: api/Search
        public void Post([FromBody]dynamic value)
        {
            Podcast new_podcast = new Podcast();
            new_podcast.FeedUrl = value.feedUrl;
            new_podcast.Title = value.title;
            new_podcast.Author = value.author;
            new_podcast.ImageUrl = value.artWork1600;
            repo.AddPodcastChannelToUserSubscriptions(GetCurrentUser(), new_podcast);
        }

        // PUT: api/Search/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Search/5
        public void Delete(int id)
        {
        }
    }
}
