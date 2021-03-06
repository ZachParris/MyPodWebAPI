﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPodWebAPI.Models
{
    public class Blog
    {
        [Key]
        public int PostId { get; set; }
        public string Post { get; set; }
        [JsonIgnoreAttribute]
        public virtual CustomUser BlogAuthor { get; set; }
    }
}