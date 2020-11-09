using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAssignment2.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [Display(Name = "Peer Reviewed")]
        public Boolean PeerReviewed { get; set; }

        [Display(Name = "Author ID")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
