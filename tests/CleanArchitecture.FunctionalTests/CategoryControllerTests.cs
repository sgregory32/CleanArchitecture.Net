﻿using CleanArchitecture.Api;
using CleanArchitecture.Api.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.FunctionalTests
{
    public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly string apiUrl = "/api/categories/";

        public CategoryControllerTests(CustomWebApplicationFactory<Startup> factory)
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
            var result = JsonConvert.DeserializeObject<CategoryDTO>(stringResponse);

            Assert.NotNull(result);
            Assert.Equal(SeedData.category1.Id, result.Id);
            Assert.Equal(SeedData.category1.Name, result.Name);
        }

        [Fact]
        public async Task GetReturnsList()
        {
            var response = await _client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(stringResponse).ToList();

            Assert.Equal(SeedData.category1.Name, result[0].Name);
            Assert.Equal(SeedData.category2.Name, result[1].Name);
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
            string jsonData = "{ \"name\":\"Post Test Category\" }";

            var response = await _client.PostAsync(apiUrl, ContentHelper.GetStringContent(jsonData));
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CategoryDTO>(stringResponse);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
            Assert.Equal("Post Test Category", result.Name);
        }
        #endregion

        #region "Put Tests"
        [Fact]
        public async Task PutReturnsNotFound()
        {
            string jsonData = "{ \"id\":1,\"name\":\"Post Test Category\" }";
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
            string jsonData = "{ \"id\":-1,\"name\":\"Post Test Category\" }";
            var response = await _client.PutAsync(apiUrl + 1, ContentHelper.GetStringContent(jsonData));
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task PutReturnsNoContent()
        {
            string jsonData = "{ \"id\":1,\"name\":\"Post Test Category\" }";
            var response = await _client.PutAsync(apiUrl + 1, ContentHelper.GetStringContent(jsonData));
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);

            response = await _client.GetAsync(apiUrl + 1);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CategoryDTO>(stringResponse);
            Assert.Equal("Post Test Category", result.Name);
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
