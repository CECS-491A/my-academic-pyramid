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
            Departments = new List<Department>();
        }

        public SearchResponse(List<SearchPersonDTO> people)
        {
            People = people;
            Departments = new List<Department>();
        }

        public SearchResponse(List<SearchPersonDTO> people, string message)
        {
            People = people;
            Message = message;
            Departments = new List<Department>();
        }

        public SearchResponse(List<Department> departments)
        {
            Departments = departments;
            People = new List<SearchPersonDTO>();
        }

        public List<SearchPersonDTO> People { get; set; }
        public List<Department> Departments { get; set; }
        public string Message { get; set; }
    }
}
