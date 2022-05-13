using System.ComponentModel.DataAnnotations;

namespace NinthProject
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
