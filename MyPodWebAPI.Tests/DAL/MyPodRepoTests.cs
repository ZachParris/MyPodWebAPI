using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Moq;
using MyPod.Models;
using MyPod.DAL;
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
        private Mock<DbSet<Message>> mock_messages;

        private Mock<DbSet<ApplicationUser>> mock_users { get; set; }
        private Mock<MyPodContext> mock_context { get; set; }
        private MyPodRepository Repo { get; set; }
        private List<ApplicationUser> users { get; set; }
        private List<Podcast> podcasts { get; set; }
        public List<Message> messages { get; private set; }
        public List<Episode> episodes { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<MyPodContext>();
            mock_users = new Mock<DbSet<ApplicationUser>>();
            mock_podcasts = new Mock<DbSet<Podcast>>();
            mock_episodes = new Mock<DbSet<Episode>>();
            mock_messages = new Mock<DbSet<Message>>();
            Repo = new MyPodRepository(mock_context.Object);

            messages = new List<Message>();
            episodes = new List<Episode>();
            podcasts = new List<Podcast>();
            ApplicationUser paulyD = new ApplicationUser { Email = "paulyD@example.com", Id = "1234567" };
            ApplicationUser mikeD = new ApplicationUser { Email = "mikeyD@example.com", Id = "1234569" };
            users = new List<ApplicationUser>()
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
            var query_messages = messages.AsQueryable();

            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(query_users.Provider);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(query_users.Expression);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(query_users.ElementType);
            mock_users.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(() => query_users.GetEnumerator());

            mock_context.Setup(c => c.Users).Returns(mock_users.Object);
            mock_users.Setup(u => u.Add(It.IsAny<ApplicationUser>())).Callback((ApplicationUser t) => users.Add(t));

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

            mock_messages.As<IQueryable<Message>>().Setup(m => m.Provider).Returns(query_messages.Provider);
            mock_messages.As<IQueryable<Message>>().Setup(m => m.Expression).Returns(query_messages.Expression);
            mock_messages.As<IQueryable<Message>>().Setup(m => m.ElementType).Returns(query_messages.ElementType);
            mock_messages.As<IQueryable<Message>>().Setup(m => m.GetEnumerator()).Returns(() => query_messages.GetEnumerator());

            mock_context.Setup(c => c.Messages).Returns(mock_messages.Object);
            mock_messages.Setup(u => u.Add(It.IsAny<Message>())).Callback((Message t) => messages.Add(t));
        }

        [TestMethod]
        public void RepoEnsureCanCreateAnInstance()
        {
            MyPodRepository repo = new MyPodRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepoEnsureCanSubscribeToPodcasts()
        {
            ConnectToDatastore();
            Repo.AddPodcastToUser("trentS", "thejoeroganexperience");

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