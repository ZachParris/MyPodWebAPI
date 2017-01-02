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
    }
}