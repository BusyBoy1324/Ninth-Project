using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinthProject;
using System.Collections.Generic;
using System.Linq;
using static NinthProjectTests.Utilities;

namespace NinthProjectTests
{
    [TestClass]
    public class NinthProjectTests
    {
        CourseRepos _coursesServices;
        GroupRepos _groupsServices;
        StudentRepos _studentServices;
        NinthProjectContext context;
        WebApplicationFactory<Program> factory;

        [TestInitialize]
        public void Initialize()
        {
            this.factory = new CustomWebApplicationFactory<Program>();
            var scope = this.factory.Services.CreateScope();
            context = scope.ServiceProvider.GetRequiredService<NinthProjectContext>();
            this._coursesServices = new CourseRepos(context);
            this._groupsServices = new GroupRepos(context);
            this._studentServices = new StudentRepos(context);
        }

        [TestMethod]
        public void Courses_AddNewCourse_Returns_Courses()
        {
            Course expectedCourse = new Course
            {
                CourseName = "VirtualTestAdd",
                CourseDescription = "VirtualTestAdd_description"
            };
            _coursesServices.Insert(expectedCourse);
            context.SaveChanges();
            Assert.AreEqual(expectedCourse, _coursesServices.GetAll().FirstOrDefault(expectedCourse));
        }
        [TestMethod]
        public void Courses_CourseGetById_Returns_Course()
        {
            var expected = _coursesServices.GetAll(); //Where(c => c.CourseName == "SR").FirstOrDefault();
            if (expected == null)
            {
                Assert.Fail("Expected is null");
            }

            /*var actual = _coursesServices.GetById(expected.CourseId);

            if (actual == null)
            {
                Assert.Fail($"Actual is null");
            }

            Assert.AreEqual(expected.CourseName, actual.CourseName);*/
            Assert.Fail("end");
        }
        [TestMethod]
        public void Courses_CoursesGetAny_Returns_Boolean()
        {
            Course expectedCourse = new Course
            {
                CourseName = "VirtualTestGetAny",
                CourseDescription = "VirtualTestGetAny_description"
            };
            _coursesServices.Insert(expectedCourse);
            context.SaveChanges();
            bool actual = _coursesServices.GetAny(expectedCourse.CourseId);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Courses_DeleteCourse_Returns_Courses()
        {
            Course expectedCourse = new Course
            {
                CourseName = "VirtualTestDelete",
                CourseDescription = "VirtualTestDelete_description"
            };
            _coursesServices.Insert(expectedCourse);
            context.SaveChanges();
            int expected = _coursesServices.GetAll().Count() - 1;
            _coursesServices.Delete(expectedCourse);
            context.SaveChanges();
            Assert.AreEqual(expected, _coursesServices.GetAll().Count);
        }
        [TestMethod]
        public void Courses_RelatedGroups_Returns_Groups()
        {
            Course expectedCourse = new Course
            {
                CourseName = "VirtualTestGetRelatedGroups",
                CourseDescription = "VirtualTestGetRelatedGroups_description"
            };
            _coursesServices.Insert(expectedCourse);
            context.SaveChanges();
            int courseId = -1;
            int groupId = -1;
            foreach (var course in _coursesServices.GetAll())
            {
                if (course.CourseName == expectedCourse.CourseName && expectedCourse.CourseDescription == course.CourseDescription)
                {
                    courseId = course.CourseId;
                }
            }
            Groups expectedGroup = new Groups
            {
                CourseId = courseId,
                GroupName = "VirtualTestGetRelatedGroups_groupname"
            };
            _groupsServices.Insert(expectedGroup);
            context.SaveChanges();
            foreach (var group in _groupsServices.GetAll())
            {
                if (group.GroupName == expectedGroup.GroupName)
                {
                    groupId = group.GroupId;
                }
            }
            List<Groups>? expectedGroups = new List<Groups>();
            expectedGroup.GroupId = groupId;
            expectedGroups.Add(expectedGroup);
            var actual = _coursesServices.GetRelatedGroups(courseId);
            bool equal = true;
            for (int i = 0; i < actual.Count; i++)
            {
                if (actual[i] != expectedGroups[i])
                {
                    equal = false;
                    break;
                }
            }

            Assert.AreEqual(true, equal);
        }
        [TestMethod]
        public void Courses_UpdateCourse_Returnes_Updated()
        {
            Course expectedCourse = new Course
            {
                CourseName = "VirtualTestUpdate",
                CourseDescription = "VirtualTestUpdate_description"
            };
            _coursesServices.Insert(expectedCourse);
            context.SaveChanges();
            foreach (var course in _coursesServices.GetAll())
            {
                if (course.CourseName == expectedCourse.CourseName && expectedCourse.CourseDescription == course.CourseDescription)
                {
                    expectedCourse.CourseId = course.CourseId;
                    break;
                }
            }
            expectedCourse.CourseName = "VirtualCasinoVulkan";
            expectedCourse.CourseDescription = "Zahodi poskoree, pro[censored] khm... potrat` vsio bablo";
            _coursesServices.Update(expectedCourse);
            var actual = _coursesServices.GetById(expectedCourse.CourseId);
            Assert.AreEqual(expectedCourse, actual);
        }
        //[TestCleanup]
        //public void ClearToGlassClearly()
        //{
        //    var allStudents = _studentServices.GetAll();
        //    foreach (var student in allStudents)
        //    {
        //        _studentServices.Delete(student);
        //    }
        //    context.SaveChanges();
        //    var allGroups = _groupsServices.GetAll();
        //    foreach (var group in allGroups)
        //    {
        //        _groupsServices.Delete(group);
        //    }
        //    context.SaveChanges();
        //    var allCourses = _coursesServices.GetAll();
        //    foreach (var course in allCourses)
        //    {
        //        _coursesServices.Delete(course);
        //    }
        //    context.SaveChanges();
        //    Assert.AreEqual(true, !false);
        //}
    }
}