#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NinthProject.Data;
using NinthProject.Models;

namespace NinthProject.Controllers
{
    public class GroupsController : Controller
    {
        private readonly NinthProjectContext _context;

        public GroupsController(NinthProjectContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            return View(await _context.Groups.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,CourseId,GroupName")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groups);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groups);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups.FindAsync(id);
            if (groups == null)
            {
                return NotFound();
            }
            return View(groups);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,CourseId,GroupName")] Groups groups)
        {
            if (id != groups.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groups);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupsExists(groups.GroupId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(groups);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (groups == null)
            {
                return NotFound();
            }
            //var students = _context.Students.Where(s => s.GroupId == id).ToList<Students>();
            //if (students.Count > 0)
            //{
            //    return NotFound();
            //}

            return View(groups);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var students = _context.Students.Where(s => s.GroupId == id).ToList<Students>();
            if (students.Count > 0)
            {
                ViewBag.Message = "Error! Group has students!";
            }
            else
            {
                var groups = await _context.Groups.FindAsync(id);
                _context.Groups.Remove(groups);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GroupsExists(int id)
        {
            return _context.Groups.Any(e => e.GroupId == id);
        }
        // GET: Groups/Related/5
        public async Task<IActionResult> RelatedStudents(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(_context.Students.Where(j => j.GroupId == id).ToList<Students>());
        }
    }
}
