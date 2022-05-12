using Microsoft.EntityFrameworkCore;
using NinthProject.Models;

namespace NinthProject.Infrastructure
{
    public interface IGroupRepos
    {
        IList<Groups> GetAll();
        Groups GetById(int? id);
        void Insert(Groups group);
        void Update(Groups group);
        void Delete(Groups group);
        DbSet<Courses> GetDbSetCourses();
        Groups Find(int? id);
        List<Students> GetRelatedStudents(int? id);
        bool GetAny(int id);
    }
}
