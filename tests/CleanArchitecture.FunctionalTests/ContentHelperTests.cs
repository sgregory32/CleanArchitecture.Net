using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace CleanArchitecture.FunctionalTests
{
    public class ContentHelperTests
    {
        readonly string jsonData = "{ \"name\":\"Post Test Category\" }";

        [Fact]
        public void ContentHelperReturnsStringContentType()
        {
            var result = ContentHelper.GetStringContent(jsonData);
            Assert.NotNull(result);
            Assert.IsType<StringContent>(result);
            Assert.Equal("utf-8", result.Headers.ContentType.CharSet);
            Assert.Equal("application/json", result.Headers.ContentType.MediaType);
        }
    }
}
