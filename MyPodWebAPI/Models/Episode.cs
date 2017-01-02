using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPodWebAPI.Models
{
    public class Episode
    {
        [Key]
        public int EpisodeId { get; set; }
        public int PodcastId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string AirDate { get; set; }
        public string URL { get; set; }
    }
}