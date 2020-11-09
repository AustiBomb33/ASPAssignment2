using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAssignment2.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Display(Name = "Author Name")]
        public String AuthorName { get; set; }
        public List<Article> Articles { get; set; }
        [Display(Name = "Account ID")]
        public string AccountID { get; set; }
    }
}
