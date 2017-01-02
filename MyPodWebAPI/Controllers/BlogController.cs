using Microsoft.AspNet.Identity;
using MyPodWebAPI.DAL;
using MyPodWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static MyPodWebAPI.Models.MyPodViewModel;

namespace MyPodWebAPI.Controllers
{
    public class BlogController : ApiController
    {
        [HttpGet]
        [Route("api/blog")]
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("api/blog")]
        // POST api/<controller>
        public Dictionary<string, bool> Post([FromBody]BlogViewModel value)
        {
            MyPodRepo repo = new MyPodRepo();
            Dictionary<string, bool> post = new Dictionary<string, bool>();
            if (ModelState.IsValid)
            {
                Blog new_post = new Blog
                {
                    Post = value.BlogPost
                };
                var userId = User.Identity.GetUserId();
                repo.AddBlogPost(new_post);
                post.Add("successful", true);
            }
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