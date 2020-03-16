using CleanArchitecture.Api;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Core.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.FunctionalTests
{
    public class ProductControllerTests : IClassFixture<AppDbContext<TStartup>>
    {
        private readonly HttpClient _client;
        private readonly string apiUrl = "/api/products/";

        public ProductControllerTests(AppDbContext<TStartup> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region "Get Tests"
        [Fact]
        public async Task GetReturnsBadRequest()
        {
            var response = await _client.GetAsync(apiUrl + "a");
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetReturnsNotFound()
        {
            var response = await _client.GetAsync(apiUrl + 32);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetByIdReturnsItem()
        {
            var response = await _client.GetAsync(apiUrl + "1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Product>(stringResponse);

            Assert.NotNull(result);
            Assert.Equal(SeedData.product1.Id, result.Id);
            Assert.Equal(SeedData.product1.Name, result.Name);
            Assert.Equal(SeedData.product1.Description, result.Description);
            Assert.Equal(SeedData.product1.CategoryId, result.CategoryId);
            Assert.NotNull(result.Category);
        }

        [Fact]
        public async Task GetReturnsList()
        {
            var response = await _client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Product>>(stringResponse).ToList();

            Assert.NotNull(result);
            Assert.Equal(SeedData.product1.Name, result[0].Name);
            Assert.Equal(SeedData.product1.Description, result[0].Description);
            Assert.Equal(SeedData.product1.CategoryId, result[0].CategoryId);
            Assert.NotNull(result[0].Category);
            Assert.Equal(SeedData.product2.Name, result[1].Name);
            Assert.Equal(SeedData.product2.Description, result[1].Description);
            Assert.Equal(SeedData.product2.CategoryId, result[1].CategoryId);
            Assert.NotNull(result[1].Category);
        }
        #endregion

        #region "PostTests"
        [Fact]
        public async Task PostReturnsBadRequest()
        {
            string jsonData = "{ \"name\":null }";

            var response = await _client.PostAsync(apiUrl, ContentHelper.GetStringContent(jsonData));
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostReturnsCreated()
        {
            string jsonData = "{ \"name\":\"Post Test Product Name\",\"description\":\"Post Test Product Description\",\"categoryId\":1 }";

            var response = await _client.PostAsync(apiUrl, ContentHelper.GetStringContent(jsonData));
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProductDTO>(stringResponse);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);

            Assert.Equal("Post Test Product Name", result.Name);
            Assert.Equal("Post Test Product Description", result.Description);
            Assert.Equal(1, result.CategoryId);
        }
        #endregion

        #region "Put Tests"
        [Fact]
        public async Task PutReturnsNotFound()
        {
            string jsonData = "{ \"id\":1,\"name\":\"Put Test Product Name\",\"description\":\"Put Test Product Description\",\"categoryId\":1}";

            var response = await _client.PutAsync(apiUrl + 32, ContentHelper.GetStringContent(jsonData));
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PutReturnsMethodNotAllowed()
        {
            var response = await _client.DeleteAsync(apiUrl);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed);
        }

        [Fact]
        public async Task PutReturnsInternalServerError()
        {
            string jsonData = "{ \"id\":-1,\"name\":\"Put Test Product Name\",\"description\":\"Put Test Product Description\",\"categoryId\":1}";

            var response = await _client.PutAsync(apiUrl + 1, ContentHelper.GetStringContent(jsonData));
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task PutReturnsNoContent()
        {
            string jsonData = "{ \"id\":3,\"name\":\"Put Test Product Name\",\"description\":\"Put Test Product Description\",\"categoryId\":1}";
            var response = await _client.PutAsync(apiUrl + 3, ContentHelper.GetStringContent(jsonData));
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);

            response = await _client.GetAsync(apiUrl + 3);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProductDTO>(stringResponse);
            Assert.Equal("Put Test Product Name", result.Name);
            Assert.Equal("Put Test Product Description", result.Description);
            Assert.Equal(1, result.CategoryId);
        }
        #endregion

        #region "Delete Tests"
        [Fact]
        public async Task DeleteReturnsNotFound()
        {
            var response = await _client.DeleteAsync(apiUrl + 32);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteReturnsMethodNotAllowed()
        {
            var response = await _client.DeleteAsync(apiUrl);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed);
        }

        [Fact]
        public async Task DeleteReturnsNoContent()
        {
            var response = await _client.DeleteAsync(apiUrl + 3);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            response = await _client.GetAsync(apiUrl + 3);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }
        #endregion
    }
}
