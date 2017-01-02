using Microsoft.AspNet.Identity.EntityFramework;
using MyPodWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyPodWebAPI.DAL
{
    public class MyPodContext : IdentityDbContext<CustomUser>
    {
        public MyPodContext() : base("MyPodContext") { }
        public virtual DbSet<Podcast> Podcasts { get; set; }
        public virtual DbSet<Episode> Episodes { get; set; }
        public virtual DbSet<Blog> Posts { get; set; }
    }
}