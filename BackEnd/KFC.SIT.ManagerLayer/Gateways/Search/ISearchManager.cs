﻿using DataAccessLayer.DTOs;
using DataAccessLayer.Models.Requests;
using DataAccessLayer.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Gateways.Search
{
    public interface ISearchManager
    {
        dynamic Search (SearchRequest request, int category);
        List<DepartmentDTO> GetDepartments(string request);
    }
}
