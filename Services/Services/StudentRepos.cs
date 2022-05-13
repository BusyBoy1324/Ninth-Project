using Microsoft.EntityFrameworkCore;

namespace NinthProject
{
    public class StudentRepos : IStudentRepos
    {
        private NinthProjectContext _context;

        public StudentRepos(NinthProjectContext context)
        {
            _context = context;
        }

        public void Delete(Students student)
        {
            _context.Students.Remove(student);
        }

        public Students Find(int? id)
        {
            return _context.Students.Find(id);
        }

        public IList<Students> GetAll()
        {
            return _context.Students.ToList();
        }

        public bool GetAny(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }

        public Students GetById(int? id)
        {
            var student = _context.Students.Where(x => x.StudentId == id).FirstOrDefault();
            return student;
        }

        public DbSet<Groups> GetDbSetGroups()
        {
            return _context.Groups;
        }

        public void Insert(Students student)
        {
            _context.Students.Add(student);
        }

        public void Update(Students student)
        {
            _context.Students.Update(student);
        }
    }
}