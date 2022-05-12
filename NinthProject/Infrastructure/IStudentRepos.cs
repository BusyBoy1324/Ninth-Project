using Microsoft.EntityFrameworkCore;
using NinthProject.Models;

namespace NinthProject.Infrastructure
{
    public interface IStudentRepos
    {
        IList<Students> GetAll();
        Students GetById(int? id);
        void Insert (Students student);
        void Update (Students student);
        void Delete (Students student);
        DbSet<Groups> GetDbSetGroups();
        Students Find(int? id);
        bool GetAny(int id);
    }
}
