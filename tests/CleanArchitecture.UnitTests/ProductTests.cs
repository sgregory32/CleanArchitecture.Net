using CleanArchitecture.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanArchitecture.UnitTests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ProductConstructorTest()
        {
            Product product = new Product();
            Assert.IsNotNull(product);
        }
    }
}
