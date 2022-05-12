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
    public class GroupsController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public GroupsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            return View(_unitOfWork.GroupRepos.GetAll());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = _unitOfWork.GroupRepos.GetById(id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            ViewBag.CourseId = new SelectList(_unitOfWork.GroupRepos.GetDbSetCourses(), "CourseId", "CourseId");

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
                _unitOfWork.GroupRepos.Insert(groups);
                _unitOfWork.Save();
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

            var groups = _unitOfWork.GroupRepos.Find(id);
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
                    _unitOfWork.GroupRepos.Update(groups);
                    _unitOfWork.Save();
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

            var groups = _unitOfWork.GroupRepos.GetById(id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var students = _unitOfWork.GroupRepos.GetRelatedStudents(id);
            if (students.Count > 0)
            {
                ViewBag.Message = "Error! Group has students!";
            }
            else
            {
                var groups = _unitOfWork.GroupRepos.Find(id);
                _unitOfWork.GroupRepos.Delete(groups);
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GroupsExists(int id)
        {
            return _unitOfWork.GroupRepos.GetAny(id);
        }
        // GET: Groups/Related/5
        public async Task<IActionResult> RelatedStudents(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = _unitOfWork.GroupRepos.GetById(id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(_unitOfWork.GroupRepos.GetRelatedStudents(id));
        }
    }
}
