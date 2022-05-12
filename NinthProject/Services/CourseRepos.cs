using NinthProject.Data;
using NinthProject.Infrastructure;
using NinthProject.Models;

namespace NinthProject.Services
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

        public void Delete(Courses course)
        {
            _context.Courses.Remove(course);
        }

        public Courses Find(int? id)
        {
            var course = _context.Courses.Find(id);
            return course;
        }

        public IList<Courses> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Courses GetById(int? id)
        {
            var course = _context.Courses.Where(x => x.CourseId == id).FirstOrDefault();
            return course;
        }

        public List<Groups> GetRelatedGroups(int? id)
        {
            var relatedGroups = _context.Groups.Where(j => j.CourseId == id).ToList<Groups>();
            return relatedGroups;
        }

        public void Insert(Courses course)
        {
            _context.Courses.Add(course);
        }

        public void Update(Courses course)
        {
            _context.Courses.Update(course);
        }
    }
}
