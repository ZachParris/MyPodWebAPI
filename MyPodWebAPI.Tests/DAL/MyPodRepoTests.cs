using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Moq;
using MyPodWebAPI.Models;
using MyPodWebAPI.DAL;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace MyPod.Tests.DAL
{
    [TestClass]
    public class MyPodRepoTests
    {
        private Mock<DbSet<Podcast>> mock_podcasts;
        private Mock<DbSet<Episode>> mock_episodes;
        private Mock<DbSet<Blog>> mock_Blogs;

        private Mock<DbSet<CustomUser>> mock_users { get; set; }
        private Mock<MyPodContext> mock_context { get; set; }
        private MyPodRepo Repo { get; set; }
        private List<CustomUser> users { get; set; }
        private List<Podcast> podcasts { get; set; }
        public List<Blog> Blogs { get; private set; }
        public List<Episode> episodes { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<MyPodContext>();
            mock_users = new Mock<DbSet<CustomUser>>();
            mock_podcasts = new Mock<DbSet<Podcast>>();
            mock_episodes = new Mock<DbSet<Episode>>();
            mock_Blogs = new Mock<DbSet<Blog>>();
            Repo = new MyPodRepo(mock_context.Object);

            Blogs = new List<Blog>();
            episodes = new List<Episode>();
            podcasts = new List<Podcast>();
            CustomUser paulyD = new CustomUser { Email = "paulyD@example.com", Id = "1234567" };
            CustomUser mikeD = new CustomUser { Email = "mikeyD@example.com", Id = "1234569" };
            users = new List<CustomUser>()
            {
                paulyD,
                mikeD
            };
        }

        public void ConnectToDatastore()
        {
            var query_users = users.AsQueryable();
            var query_podcasts = podcasts.AsQueryable();
            var query_episodes = episodes.AsQueryable();
            var query_Blogs = Blogs.AsQueryable();

            mock_users.As<IQueryable<CustomUser>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<CustomUser>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<CustomUser>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<CustomUser>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            mock_context.Setup(c => c.Users).Returns(mock_users.Object);
            mock_users.Setup(u => u.Add(It.IsAny<CustomUser>())).Callback((CustomUser t) => users.Add(t));

            mock_podcasts.As<IQueryable<Podcast>>().Setup(m => m.Provider).Returns(query_podcasts.Provider);
            mock_podcasts.As<IQueryable<Podcast>>().Setup(m => m.Expression).Returns(query_podcasts.Expression);
            mock_podcasts.As<IQueryable<Podcast>>().Setup(m => m.ElementType).Returns(query_podcasts.ElementType);
            mock_podcasts.As<IQueryable<Podcast>>().Setup(m => m.GetEnumerator()).Returns(() => query_podcasts.GetEnumerator());

            mock_context.Setup(c => c.Podcasts).Returns(mock_podcasts.Object);
            mock_podcasts.Setup(u => u.Add(It.IsAny<Podcast>())).Callback((Podcast t) => podcasts.Add(t));

            mock_episodes.As<IQueryable<Episode>>().Setup(m => m.Provider).Returns(query_episodes.Provider);
            mock_episodes.As<IQueryable<Episode>>().Setup(m => m.Expression).Returns(query_episodes.Expression);
            mock_episodes.As<IQueryable<Episode>>().Setup(m => m.ElementType).Returns(query_episodes.ElementType);
            mock_episodes.As<IQueryable<Episode>>().Setup(m => m.GetEnumerator()).Returns(() => query_episodes.GetEnumerator());

            mock_context.Setup(c => c.Episodes).Returns(mock_episodes.Object);
            mock_episodes.Setup(u => u.Add(It.IsAny<Episode>())).Callback((Episode t) => episodes.Add(t));

            mock_Blogs.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(query_Blogs.Provider);
            mock_Blogs.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(query_Blogs.Expression);
            mock_Blogs.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(query_Blogs.ElementType);
            mock_Blogs.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(() => query_Blogs.GetEnumerator());

            mock_context.Setup(c => c.Posts).Returns(mock_Blogs.Object);
            mock_Blogs.Setup(u => u.Add(It.IsAny<Blog>())).Callback((Blog t) => Blogs.Add(t));
        }

        [TestMethod]
        public void RepoEnsureCanCreateAnInstance()
        {
            MyPodRepo repo = new MyPodRepo();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepoEnsureCanSubscribeToPodcasts()
        {
            ConnectToDatastore();
            //Repo.AddPodcastToUser("trentS", "thejoeroganexperience");

            int expected_podcasts = 1;
            //int actual_podcasts = Repo.GetPodcasts().Count;

            //Assert.AreEqual(expected_podcasts, actual_podcasts);
        }

        [TestMethod]
        public void RepoEnsureCanRemovePodcastSubscription()
        {

        }


    }
}