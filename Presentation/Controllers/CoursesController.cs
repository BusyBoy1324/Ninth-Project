#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace NinthProject.Controllers
{
    public class CoursesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(_unitOfWork.CoursesRepos.GetAll());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courses = _unitOfWork.CoursesRepos.GetById(id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,CourseDescription")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoursesRepos.Insert(courses);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(courses);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courses = _unitOfWork.CoursesRepos.Find(id);
            if (courses == null)
            {
                return NotFound();
            }
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseName,CourseDescription")] Courses courses)
        {
            if (id != courses.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CoursesRepos.Update(courses);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursesExists(courses.CourseId))
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
            return View(courses);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courses = _unitOfWork.CoursesRepos.GetById(id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courses = _unitOfWork.CoursesRepos.Find(id);
            _unitOfWork.CoursesRepos.Delete(courses);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursesExists(int id)
        {
            return _unitOfWork.CoursesRepos.GetAny(id);
        }
        // GET: Courses/Related/5
        public async Task<IActionResult> RelatedGroups(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courses = _unitOfWork.CoursesRepos.GetById(id);
            if (courses == null)
            {
                return NotFound();
            }

            var relatedGroups = _unitOfWork.CoursesRepos.GetRelatedGroups(id);
            return View(relatedGroups);
        }
    }
}
