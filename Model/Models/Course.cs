using System.ComponentModel.DataAnnotations;

namespace NinthProject
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
    }
}
