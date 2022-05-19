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
        StudentRepos _studentsServices;
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
            this._studentsServices = new StudentRepos(context);
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
            Assert.AreSame(expectedCourse, _coursesServices.GetById(expectedCourse.CourseId));
        }
        [TestMethod]
        public void Courses_CourseGetById_Returns_Course()
        {
            var expected = context.Courses.Where(c => c.CourseName == "SR").FirstOrDefault();

            var actual = _coursesServices.GetById(expected.CourseId);

            Assert.AreEqual(expected.CourseName, actual.CourseName);
        }
        [TestMethod]
        public void Courses_CoursesGetAny_Returns_Boolean()
        {
            var course = context.Courses.Where(n => n.CourseName == "FT").FirstOrDefault();

            bool expected = true;

            bool actual = _coursesServices.GetAny(course.CourseId);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Courses_DeleteCourse_Returns_Courses()
        {
            var course = context.Courses.Where(n => n.CourseName == "VC").FirstOrDefault();

            _coursesServices.Delete(course);
            context.SaveChanges();
            bool expected = false;
            bool actual = _coursesServices.GetAny(course.CourseId);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Courses_RelatedGroups_Returns_Groups()
        {
            var course = context.Courses.Where(i => i.CourseId == 2).FirstOrDefault();
            var expected = context.Groups.Where(i => i.CourseId == 2).ToList();

            var actual = _coursesServices.GetRelatedGroups(course.CourseId);

            CollectionAssert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void Courses_UpdateCourse_Returnes_Updated()
        {
            var expected = context.Courses.Where(n => n.CourseName == "SR").FirstOrDefault();
            expected.CourseName = "MH";
            expected.CourseDescription = "Miners";
            _coursesServices.Update(expected);

            var actual = _coursesServices.GetById(expected.CourseId);

            Assert.AreEqual(expected.CourseName, actual.CourseName);
        }

        [TestMethod]
        public void Groups_AddNewGroup_Returns_Group()
        {
            Groups expectedGroup = new Groups
            {
                GroupName = "VirtualTestAdd",
                CourseId = 1
            };
            _groupsServices.Insert(expectedGroup);
            context.SaveChanges();
            Assert.AreEqual(expectedGroup.GroupName, _groupsServices.GetById(expectedGroup.GroupId).GroupName);
        }
        [TestMethod]
        public void Groups_GroupGetById_Returns_Group()
        {
            var expected = context.Groups.Where(c => c.GroupName == "SR-01").FirstOrDefault();

            var actual = _groupsServices.GetById(expected.GroupId);

            Assert.AreEqual(expected.GroupName, actual.GroupName);
        }
        [TestMethod]
        public void Groups_GroupsGetAny_Returns_Boolean()
        {
            var group = context.Groups.Where(n => n.GroupName == "FT-01").FirstOrDefault();

            bool expected = true;

            bool actual = _groupsServices.GetAny(group.GroupId);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Groups_DeleteGroup_Returns_Groups()
        {
            var group = context.Groups.Where(n => n.GroupName == "SR-02").FirstOrDefault();

            _groupsServices.Delete(group);
            context.SaveChanges();
            bool expected = false;
            bool actual = _groupsServices.GetAny(group.GroupId);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Groups_RelatedStudents_Returns_Students()
        {
            var group = context.Groups.Where(i => i.GroupId == 1).FirstOrDefault();
            var expected = context.Students.Where(i => i.GroupId == 1).ToList();

            var actual = _groupsServices.GetRelatedStudents(group.GroupId);

            CollectionAssert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void Groups_UpdateGroup_Returnes_Updated()
        {
            var expected = context.Groups.Where(n => n.GroupName == "SR-01").FirstOrDefault();
            expected.GroupName = "MH-01";
            _groupsServices.Update(expected);

            var actual = _groupsServices.GetById(expected.GroupId);

            Assert.AreEqual(expected.GroupName, actual.GroupName);
        }
        [TestMethod]
        public void Students_AddNewStudent_Returns_Student()
        {
            Students expectedStudent = new Students
            {
                FirstName = "VirtualTestAdd",
                LastName = "VirtualTestAdd",
                GroupId = 1
            };
            _studentsServices.Insert(expectedStudent);
            context.SaveChanges();
            Assert.AreEqual(expectedStudent.FirstName, _studentsServices.GetById(expectedStudent.StudentId).FirstName);
        }
        [TestMethod]
        public void Students_StudentGetById_Returns_Student()
        {
            var expected = context.Students.Where(c => c.FirstName == "Mikyta").FirstOrDefault();

            var actual = _studentsServices.GetById(expected.StudentId);

            Assert.AreEqual(expected.FirstName, actual.FirstName);
        }
        [TestMethod]
        public void Students_StudentGetAny_Returns_Boolean()
        {
            var student = context.Students.Where(n => n.FirstName == "Mikyta").FirstOrDefault();

            bool expected = true;

            bool actual = _studentsServices.GetAny(student.StudentId);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Students_DeleteStudent_Returns_Students()
        {
            var student = context.Students.Where(n => n.FirstName == "Mikyta").FirstOrDefault();

            _studentsServices.Delete(student);
            context.SaveChanges();
            bool expected = false;
            bool actual = _studentsServices.GetAny(student.StudentId);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Students_UpdateStudent_Returnes_Updated()
        {
            var expected = context.Students.Where(n => n.FirstName == "Mikyta").FirstOrDefault();
            expected.FirstName = "Maxim";
            _studentsServices.Update(expected);

            var actual = _studentsServices.GetById(expected.StudentId);

            Assert.AreEqual(expected.FirstName, actual.FirstName);
        }
    }
}