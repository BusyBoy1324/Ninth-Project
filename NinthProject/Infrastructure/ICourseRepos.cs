using NinthProject.Models;

namespace NinthProject.Infrastructure
{
    public interface ICourseRepos
    {
        IList<Courses> GetAll();
        Courses GetById(int? id);
        void Insert(Courses course);
        void Update(Courses course);
        void Delete(Courses course);
        List<Groups> GetRelatedGroups(int? id);
        Courses Find(int ? id);
        bool GetAny (int id);
    }
}
