using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPodWebAPI.Models
{
    public class MyPodViewModel
    {
        public class BlogViewModel
        {
            [Required]
            public string BlogPost { get; set; }
        }

        public class PodcastViewModel
        {
            [Required]
            public string CollectionName { get; set; }
            [Required]
            public string Auther { get; set; }
            [Required]
            public bool IsSubscribed { get; set; }
            [Required]
            public string ArtworkUrl { get; set; }
        }
    }
}