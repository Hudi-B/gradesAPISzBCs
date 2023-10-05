using grades.Models;
using static grades.Controllers.DTO;

namespace grades
{
    public static class Extensions
    {
        public static GradeDto AsDto(this Grade grades)
        {
            return new GradeDto(grades.Id, grades.GradeT, grades.DescriptionT, grades.Created);
        }
    }
}
