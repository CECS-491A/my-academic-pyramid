using DataAccessLayer.DTOs;
using DataAccessLayer.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Requests
{
    public class SearchResponse
    {
        public SearchResponse()
        {
            People = new List<SearchPersonDTO>();
            ForumPosts = new List<ForumPostDTO>();
        }

        public SearchResponse(List<SearchPersonDTO> people)
        {
            People = people;
            ForumPosts = new List<ForumPostDTO>();
        }

        public SearchResponse(List<ForumPostDTO> posts)
        {
            ForumPosts = posts;
            People = new List<SearchPersonDTO>();
        }

        public SearchResponse(List<SearchPersonDTO> people, string message)
        {
            People = people;
            ForumPosts = new List<ForumPostDTO>();
            Message = message;
        }

        public SearchResponse(string message)
        {
            Message = message;
            People = new List<SearchPersonDTO>();
            ForumPosts = new List<ForumPostDTO>();
        }

        public List<SearchPersonDTO> People { get; set; }
        public List<ForumPostDTO> ForumPosts { get; set; }
        public string Message { get; set; }
    }
}
