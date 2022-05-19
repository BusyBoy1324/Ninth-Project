namespace NinthProject
{
    public class CourseRepos : ICourseRepos
    {
        private NinthProjectContext _context;

        public CourseRepos(NinthProjectContext context)
        {
            _context = context;
        }

        public bool GetAny(int id)
        {
            var anyCourses = _context.Courses.Any(x => x.CourseId == id);
            return anyCourses;
        }

        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
        }

        public IList<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course GetById(int? id)
        {
            var course = _context.Courses.Where(x => x.CourseId == id).FirstOrDefault();
            return course;
        }

        public List<Groups> GetRelatedGroups(int? id)
        {
            var relatedGroups = _context.Groups.Where(j => j.CourseId == id).ToList<Groups>();
            return relatedGroups;
        }

        public void Insert(Course course)
        {
            _context.Courses.Add(course);
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }
    }
}
