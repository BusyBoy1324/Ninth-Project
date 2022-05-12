#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NinthProject.Data;
using NinthProject.Infrastructure;
using NinthProject.Models;

namespace NinthProject.Controllers
{
    public class StudentsController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(_unitOfWork.StudentRepos.GetAll());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = _unitOfWork.StudentRepos.GetById(id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewBag.GroupId = new SelectList(_unitOfWork.StudentRepos.GetDbSetGroups(), "GroupId", "GroupId");

            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,GroupId,FirstName,LastName")] Students students)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.StudentRepos.Insert(students);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(students);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = _unitOfWork.StudentRepos.Find(id);
            if (students == null)
            {
                return NotFound();
            }
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,GroupId,FirstName,LastName")] Students students)
        {
            if (id != students.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.StudentRepos.Update(students);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.StudentId))
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
            return View(students);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = _unitOfWork.StudentRepos.GetById(id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var students = _unitOfWork.StudentRepos.Find(id);
            _unitOfWork.StudentRepos.Delete(students);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
            return _unitOfWork.StudentRepos.GetAny(id);
        }
    }
}
