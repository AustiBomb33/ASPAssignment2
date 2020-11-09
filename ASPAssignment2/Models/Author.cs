using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAssignment2.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public String AuthorName { get; set; }
        public List<Article> Articles { get; set; }
        public string AccountID { get; set; }
    }
}
