using Microsoft.EntityFrameworkCore;
using NinthProject.Data;
using NinthProject.Infrastructure;
using NinthProject.Models;

namespace NinthProject.Services
{
    public class GroupRepos : IGroupRepos
    {
        private NinthProjectContext _context;

        public GroupRepos(NinthProjectContext context)
        {
            _context = context;
        }

        public void Delete(Groups group)
        {
            _context.Groups.Remove(group);
        }

        public Groups Find(int? id)
        {
            return _context.Groups.Find(id);
        }

        public IList<Groups> GetAll()
        {
            return _context.Groups.ToList();
        }

        public bool GetAny(int id)
        {
            return _context.Groups.Any(e => e.GroupId == id);
        }

        public Groups GetById(int? id)
        {
            var group = _context.Groups.Where(x => x.GroupId == id).FirstOrDefault();
            return group;
        }

        public DbSet<Courses> GetDbSetCourses()
        {
            return _context.Courses;
        }

        public List<Students> GetRelatedStudents(int? id)
        {
            var relatedStudents = _context.Students.Where(s => s.GroupId == id).ToList<Students>();
            return relatedStudents;
        }

        public void Insert(Groups group)
        {
            _context.Groups.Add(group);
        }

        public void Update(Groups group)
        {
            _context.Groups.Update(group);
        }
    }
}
