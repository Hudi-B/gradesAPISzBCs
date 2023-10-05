using Microsoft.AspNetCore.Mvc;
using System;
using grades.Models;
using static grades.Controllers.DTO;

namespace grades.Controllers
{
    public class GradeController : Controller
    {
        Connect connect = new();
        private readonly List<GradeDto> grades = new();
    }
}
