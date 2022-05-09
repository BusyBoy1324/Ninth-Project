using System.ComponentModel.DataAnnotations;

namespace NinthProject.Models
{
    public class Groups
    {
        [Key]
        public int GroupId { get; set; }
        public int CourseId { get; set; }
        public string GroupName { get; set; }

    }
}
