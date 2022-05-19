using Microsoft.EntityFrameworkCore;

namespace NinthProject
{
    public interface IGroupRepos
    {
        IList<Groups> GetAll();
        Groups GetById(int? id);
        void Insert(Groups group);
        void Update(Groups group);
        void Delete(Groups group);
        DbSet<Course> GetDbSetCourses();
        Groups Find(int? id);
        List<Students> GetRelatedStudents(int? id);
        bool GetAny(int id);
    }
}