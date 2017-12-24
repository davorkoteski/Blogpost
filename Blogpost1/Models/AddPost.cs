using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Blogpost1.Models
{
    public class AddPost
    {
        [Key]
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Datum { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual Category Category { get; set; }
    }
}