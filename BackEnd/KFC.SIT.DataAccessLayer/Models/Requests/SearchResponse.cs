using DataAccessLayer.DTOs;
using DataAccessLayer.DTOs.SearchDTO;
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
            ForumPosts = new List<SearchForumPostDTO>();
        }

        public SearchResponse(List<SearchPersonDTO> people, string token)
        {
            People = people;
            ForumPosts = new List<SearchForumPostDTO>();
            SITtoken = token;
        }

        public SearchResponse(List<SearchForumPostDTO> posts, string token)
        {
            ForumPosts = posts;
            People = new List<SearchPersonDTO>();
            SITtoken = token;
        }

        public SearchResponse(string message)
        {
            Message = message;
            People = new List<SearchPersonDTO>();
            ForumPosts = new List<SearchForumPostDTO>();
        }

        public SearchResponse(string message, string token)
        {
            Message = message;
            People = new List<SearchPersonDTO>();
            ForumPosts = new List<SearchForumPostDTO>();
            SITtoken = token;
        }

        public List<SearchPersonDTO> People { get; set; }
        public List<SearchForumPostDTO> ForumPosts { get; set; }
        public string Message { get; set; }
        public string SITtoken { get; set; }
    }
}
