using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinthProject;

namespace NinthProjectTests
{
    [TestClass]
    public class NinthProjectTests
    {
        private IUnitOfWork _unitOfWork;

        public NinthProjectTests(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [TestMethod]
        public void TestMethod1()
        {
            int? id = 1;
            var expected = 0;
            var actual = _unitOfWork.GroupRepos.GetById(id);
            Assert.AreEqual(expected, actual);
        }
    }
}