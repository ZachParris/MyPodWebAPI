using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPodWebAPI.Models
{
    public class CustomUser : IdentityUser
    {
        public virtual List<Podcast> Subscriptions { get; set; }
        public virtual List<Blog> Posts { get; set; }

    }
}