namespace NinthProject.Infrastructure
{
    public interface IUnitOfWork
    {
        IStudentRepos StudentRepos { get; }
        IGroupRepos GroupRepos { get; }
        ICourseRepos CoursesRepos { get; }
        void Save();
    }
}
