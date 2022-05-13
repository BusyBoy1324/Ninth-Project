namespace NinthProject
{
    public class UnitOfWork : IUnitOfWork
    {
        private NinthProjectContext _context;
        private IStudentRepos _studentRepos;
        private IGroupRepos _groupRepos;
        private ICourseRepos _courseRepos;

        public UnitOfWork(NinthProjectContext context)
        {
            _context = context;
        }

        public IStudentRepos StudentRepos
        {
            get
            {
                return _studentRepos = _studentRepos ?? new StudentRepos(_context);
            }
        }

        public IGroupRepos GroupRepos
        {
            get
            {
                return _groupRepos = _groupRepos ?? new GroupRepos(_context);
            }
        }

        public ICourseRepos CoursesRepos
        {
            get
            {
                return _courseRepos = _courseRepos ?? new CourseRepos(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
