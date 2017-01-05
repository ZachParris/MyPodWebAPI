using Microsoft.AspNet.Identity;
using MyPodWebAPI.DAL;
using MyPodWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using static MyPodWebAPI.Models.MyPodViewModel;

namespace MyPodWebAPI.Controllers
{
    public class BlogController : ApiController
    {
        private MyPodRepo repo = null;

        public BlogController()
        {
            repo = new MyPodRepo();
        }

        [HttpGet]
        [Authorize]
        [Route("api/blog")]
        // GET api/<controller>
        public IEnumerable<Blog> Get()
        {
            return repo.GetBlogPosts(GetCurrentUser());
        }

        // GET api/<controller>/5
        public Blog Get(int id)
        {
            if(repo.GetBlogPosts(GetCurrentUser()).Any(p => p.PostId == id))
            {
                return repo.GetBlogById(id);
            }
            return null;
        }

        private string GetCurrentUser()
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;
            return userName;
        }

        [HttpPost]
        [Authorize]
        [Route("api/blog")]
        // POST api/<controller>
        public Dictionary<string, bool> Post([FromBody]BlogViewModel value)
        {
            Dictionary<string, bool> post = new Dictionary<string, bool>();

            if (ModelState.IsValid)
            {
                string username = GetCurrentUser();

                if (username != null)
                {
                    Blog new_post = new Blog
                    {
                        Post = value.BlogPost
                    };
                    repo.AddBlogPost(username, new_post);
                    post.Add("successful", true);
                }else
                {
                    post.Add("successful", false);
                }
            }else
            {
                post.Add("successful", false);
            }
            return post;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}