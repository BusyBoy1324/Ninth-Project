using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinthProject;
using System;
using System.Linq;

namespace NinthProjectTests
{
    public class Utilities
    {
        internal class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
        {
            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                var _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        var dbContextDescriptor = services.SingleOrDefault(d =>
                            d.ServiceType == typeof(DbContextOptions<NinthProjectContext>));
                        services.Remove(dbContextDescriptor);
                        services.AddDbContext<NinthProjectContext>(options => options.UseInMemoryDatabase("NinthProjectTestDb"));
                    });
                });

                using (var scope = _factory.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<NinthProjectContext>();

                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    try
                    {
                        InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                           "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            }
        }
        private static void InitializeDbForTests(NinthProjectContext context)
        {
            context.Courses.Add(new Course
            {
                CourseName = "SR",
                CourseDescription = "lawyers",
                CourseId = 1
            });
            context.Courses.Add(new Course
            {
                CourseName = "FT",
                CourseDescription = "food technologists",
                CourseId = 2
            });
            context.Groups.Add(new Groups
            {
                GroupName = "SR-01",
                CourseId = 1,
                GroupId = 1
            });
            context.Groups.Add(new Groups
            {
                GroupName = "SR-02",
                CourseId = 1,
                GroupId = 2
            });
            context.Groups.Add(new Groups
            {
                GroupName = "FT-01",
                CourseId = 2,
                GroupId = 3
            });
            context.Groups.Add(new Groups
            {
                GroupName = "FT-02",
                CourseId = 2,
                GroupId = 4
            });
            context.Students.Add(new Students
            {
                StudentId = 1,
                FirstName = "Alexey",
                LastName = "Varmilov",
                GroupId = 1
            });
            context.Students.Add(new Students
            {
                StudentId = 2,
                FirstName = "Mikyta",
                LastName = "Kozumyaka",
                GroupId = 1
            });
            context.Students.Add(new Students
            {
                StudentId = 3,
                FirstName = "Ivan",
                LastName = "Franko",
                GroupId = 2
            });
            context.Students.Add(new Students
            {
                StudentId = 4,
                FirstName = "Taras",
                LastName = "Shevchenko",
                GroupId = 2
            });
            context.Students.Add(new Students
            {
                StudentId = 5,
                FirstName = "Lesya",
                LastName = "Ukrainka",
                GroupId = 3
            });
            context.Students.Add(new Students
            {
                StudentId = 6,
                FirstName = "Panas",
                LastName = "Mirniy",
                GroupId = 1
            });
            context.SaveChanges();
        }
    }
}
