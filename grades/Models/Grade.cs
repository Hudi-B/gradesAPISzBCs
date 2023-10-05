﻿using Microsoft.AspNetCore.Mvc;
using grades.Models;
using static grades.Controllers.DTO;

namespace grades.Models
{
    public class Grade
    {
        public Guid Id { get; set; }
        public int GradeT { get; set; }
        public string DescriptionT { get; set; }
        public string Created { get; set; }
    }
}
