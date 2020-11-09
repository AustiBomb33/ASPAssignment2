using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAssignment2.Models
{
    public class Article
    {
        [Display(Name ="Article ID")]
        public int Id { get; set; }

        [Display(Name = "Article Title")]
        public string Title { get; set; }
        public string Content { get; set; }

        [Display(Name = "Peer Reviewed")]
        public Boolean PeerReviewed { get; set; }

        [Display(Name = "Author ID")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
