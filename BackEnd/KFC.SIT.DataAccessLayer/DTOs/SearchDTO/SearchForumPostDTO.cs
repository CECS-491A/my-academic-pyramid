using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.SearchDTO
{
    public class SearchForumPostDTO
    {
        public int postId { get; set; }
        public string action { get; set; }
        public string headline { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public List<SearchForumPostDTO> answers { get; set; }
    }
}
