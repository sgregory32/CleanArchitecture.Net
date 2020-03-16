using CleanArchitecture.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanArchitecture.UnitTests
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void CategoryConstructorTest()
        {
            Category category = new Category();
            Assert.IsNotNull(category);
        }
    }
}
