using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPodWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MyPodWebAPI.DAL
{
    public class MyPodRepo : IDisposable
    {
        private MyPodContext Context;

        private UserManager<CustomUser> _userManager;

        public MyPodRepo(MyPodContext _ctx)
        {
            Context = _ctx;
            _userManager = new UserManager<CustomUser>(new UserStore<CustomUser>(_ctx));
        }
        public MyPodRepo()
        {
            Context = new MyPodContext();
            _userManager = new UserManager<CustomUser>(new UserStore<CustomUser>(Context));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            CustomUser user = new CustomUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public List<Podcast> GetAllPodcastsForUser(string username)
        {
            return Context.Users.Where(u => u.UserName == username).FirstOrDefault().Subscriptions;
        }

        public Podcast GetPodcastById(int id)
        {
            return Context.Podcasts.SingleOrDefault(p => p.PodcastId == id);
        }

        public List<Blog> GetAllPostsForUser(string user)
        {
            return Context.Posts.Where(p => p.BlogAuthor.UserName == user).ToList();
        }

        public Blog GetBlogById(int id)
        {
            return Context.Posts.SingleOrDefault(p => p.PostId == id);
        }

        public void Dispose()
        {
            Context.Dispose();
            _userManager.Dispose();
        }

        public async Task<CustomUser> FindUser(string userName, string password)
        {
            CustomUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public object GetPodcasts(string username)
        {
            CustomUser user = Context.Users.SingleOrDefault(u => u.UserName == username);
            return Context.Users.Select(u => u.Id);
        }

        public void SubscribeToPodcast(Podcast new_podcast)
        {
            Context.Podcasts.Add(new_podcast);
            Context.SaveChanges();
        }

        public bool AddPodcastChannelToUserSubscriptions(string userId, string podcast)
        {
            Podcast found_podcast = Context.Podcasts.FirstOrDefault(p => p.Title == podcast);
            CustomUser found_user = Context.Users.FirstOrDefault(u => u.Id == userId);
            if (found_podcast != null && found_user != null)
            {
                found_user.Subscriptions.Add(found_podcast);
                Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddPodcastChannelToUserSubscriptions(string userId, Podcast podcast)
        {

            Podcast found_podcast = Context.Podcasts.FirstOrDefault(p => p.Title == podcast.Title);
            //if found podcast = null thn add podcast to DB (context.podcast.add(podcast)) 
            CustomUser found_user = Context.Users.FirstOrDefault(u => u.Id == userId);
            
            if (found_podcast != null && found_user != null)
            {
                found_user.Subscriptions.Add(found_podcast);
                Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Podcast> GetUsersPodcasts(string userId)
        {
            CustomUser user = Context.Users.SingleOrDefault(p => p.Id == userId);
            if (user.Subscriptions.Count > 0)
            {
                return user.Subscriptions;
            }
            else
            {
                return null;
            }
        }

        public void AddBlogPost(string user, Blog post)
        {
            Context.Users.SingleOrDefault(u => u.UserName == user).Posts.Add(post);
            Context.SaveChanges();
        }

        public Blog RemoveBlogPost(int blogPost_id)
        {
            Blog found_post = Context.Posts.FirstOrDefault(p => p.PostId == blogPost_id);
            if (found_post != null)
            {
                Context.Posts.Remove(found_post);
                Context.SaveChanges();
            }
            return found_post;
        }

        public List<Blog> GetBlogPosts(string username)
        {
            CustomUser user = Context.Users.SingleOrDefault(u => u.UserName == username);
            return user.Posts;
        }

        public CustomUser GetAppUser(string user_id)
        {
            return Context.Users.SingleOrDefault(u => u.Id == user_id);
        }

    }
}