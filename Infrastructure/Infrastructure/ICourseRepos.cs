namespace NinthProject
{
    public interface ICourseRepos
    {
        IList<Course> GetAll();
        Course GetById(int? id);
        void Insert(Course course);
        void Update(Course course);
        void Delete(Course course);
        List<Groups> GetRelatedGroups(int? id);
        bool GetAny(int id);
    }
}
