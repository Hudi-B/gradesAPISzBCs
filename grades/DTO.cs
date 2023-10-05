using Microsoft.AspNetCore.Mvc;

namespace grades.Controllers
{
    public class DTO
    {
        public record GradeDto(Guid Id, int GradeT, string DescriptionT, string Created);
    }
}
