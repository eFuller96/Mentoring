using System.Net;
using System.Security.Cryptography;
using System.Text;
using Flurl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoApplication.Models;
using TrenitaliaRealTimeAdapter.Acceptance.Tests;
using static System.Net.WebRequestMethods;

namespace TodoApplication.AcceptanceTests
{
    public class Tests
    {

        private readonly List<ToDoItem> _toDoItems = new List<ToDoItem>()
        {
            new()
                { Id = new Guid("7595022e-f390-4814-91ba-6db90550f46c"), Name = "string", IsCompleted = true },
            new()
                { Id = new Guid("dbfddefd-bdeb-4e5e-90bd-bccf865b6068"), Name = "update", IsCompleted = true },
            new()
                { Id = new Guid("eb22bf2c-a4aa-420d-a2c5-c3cfcb70b8bd"), Name = "string", IsCompleted = true },
            new()
                { Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Name = "string", IsCompleted = true },
        };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Get_to_do_items_from_file_storage_csv()
        {
            var acceptanceTestFixture = new AcceptanceTestsFixture(); // registration csv
            var url = new Url("http://localhost:7206/TodoList");
            var response = await acceptanceTestFixture.DoRequest(url,HttpMethod.Get);
            var result = await DeserializeResponse<ICollection<ToDoItem>>(response);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result,Is.EqualTo(_toDoItems));
        }

        // happy path
        [Test]
        public async Task Get_from_file_storage_csv()
        {
            var acceptanceTestFixture = new AcceptanceTestsFixture();
            var baseUrl = "http://localhost:7206/TodoList/";
            var url = new Url($"{baseUrl}{_toDoItems.First().Id}");
            var response = await acceptanceTestFixture.DoRequest(url, HttpMethod.Get);
            var result = await DeserializeResponse<ToDoItem>(response);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result, Is.EqualTo(_toDoItems.First()));
        }

        // happy path
        [Test]
        public async Task Delete_from_file_storage_csv()
        {
            var acceptanceTestFixture = new AcceptanceTestsFixture();
            var baseUrl = "http://localhost:7206/TodoList/";
            var url = new Url($"{baseUrl}{_toDoItems.First().Id}");
            var response = await acceptanceTestFixture.DoRequest(url, HttpMethod.Delete);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        // todo how to post
        public async Task Add_to_file_storage_csv()
        {
            var acceptanceTestFixture = new AcceptanceTestsFixture(); // registration csv
            var url = new Url("http://localhost:7206/TodoList");
            var body = new StringContent(JsonConvert.SerializeObject(_toDoItems.First()),Encoding.UTF8,"application/json");
            var response = await acceptanceTestFixture.DoRequest(url, HttpMethod.Post, body);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        // happy path
        [Test]
        public async Task Update_in_file_storage_csv()
        {
            var acceptanceTestFixture = new AcceptanceTestsFixture();
            var baseUrl = "http://localhost:7206/TodoList/";
            var url = new Url($"{baseUrl}{_toDoItems.ElementAt(2)}");
            var body = new StringContent(JsonConvert.SerializeObject(_toDoItems.First()), Encoding.UTF8, "application/json");
            var response = await acceptanceTestFixture.DoRequest(url, HttpMethod.Put,body);
            
        }

        private async Task<TResponse> DeserializeResponse<TResponse>(HttpResponseMessage response)
        {
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(responseStream);

            return JsonConvert.DeserializeObject<TResponse>(await streamReader.ReadToEndAsync());
        }
    }
}