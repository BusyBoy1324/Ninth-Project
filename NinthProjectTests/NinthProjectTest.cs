using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinthProject;
using System.Linq;
using System.Net.Http;

namespace NinthProjectTests
{
    [TestClass]
    public class NinthProjectTests
    {
        private HttpClient _testClient;

        [TestInitialize]
        public void Initialize()
        {
            var factory = new Utilities.CustomWebApplicationFactory<Program>();
            this._testClient = factory.CreateClient();
        }
        [TestMethod]
        public async void Courses_GetRelatedGroup_Returns_Groups()
        {
            //var actualGroups = _testClient.
        }
    }
}