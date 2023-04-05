using System.Net;
using System.Text;
using Flurl;
using Newtonsoft.Json;
using ToDoApplication.Models;
using TrenitaliaRealTimeAdapter.Acceptance.Tests;

namespace TodoApplication.AcceptanceTests
{
    public class ToDoItemsInCsvAcceptanceTests
    {

        private readonly List<ToDoItem> _toDoItems = new()
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

        [Test]
        public async Task Get_NonExistingItem_from_file_storage_csv()
        {
            var acceptanceTestFixture = new AcceptanceTestsFixture();
            var baseUrl = "http://localhost:7206/TodoList/";
            var url = new Url($"{baseUrl}{Guid.NewGuid()}");
            var response = await acceptanceTestFixture.DoRequest(url, HttpMethod.Get);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

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
        public async Task Delete_NonExistingItem_from_file_storage_csv()
        {
            var acceptanceTestFixture = new AcceptanceTestsFixture();
            var baseUrl = "http://localhost:7206/TodoList/";
            var nonExistingToDoItem = new ToDoItem() { Id = Guid.NewGuid(), Name = "name", IsCompleted = false };
            var url = new Url($"{baseUrl}{nonExistingToDoItem.Id}");
            var response = await acceptanceTestFixture.DoRequest(url, HttpMethod.Delete);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Add_to_file_storage_csv()
        {
            var acceptanceTestFixture = new AcceptanceTestsFixture();
            var url = new Url("http://localhost:7206/TodoList");
            var body = new StringContent(JsonConvert.SerializeObject(_toDoItems.First()),Encoding.UTF8,"application/json");
            var response = await acceptanceTestFixture.DoRequest(url, HttpMethod.Post, body);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task Update_in_file_storage_csv()
        {
            var acceptanceTestFixture = new AcceptanceTestsFixture();
            var baseUrl = "http://localhost:7206/TodoList/";
            var url = new Url($"{baseUrl}{_toDoItems.ElementAt(2).Id}");
            var body = new StringContent(JsonConvert.SerializeObject(_toDoItems.First()), Encoding.UTF8, "application/json");
            var response = await acceptanceTestFixture.DoRequest(url, HttpMethod.Put, body);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task Update_NonExistingItem_in_file_storage_csv()
        {
            var acceptanceTestFixture = new AcceptanceTestsFixture();
            var baseUrl = "http://localhost:7206/TodoList/";
            var nonExistingToDoItem = new ToDoItem() { Id = Guid.NewGuid(), Name = "name", IsCompleted = false };
            var url = new Url($"{baseUrl}{nonExistingToDoItem.Id}");
            var body = new StringContent(JsonConvert.SerializeObject(nonExistingToDoItem), Encoding.UTF8, "application/json");
            var response = await acceptanceTestFixture.DoRequest(url, HttpMethod.Put, body);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        private async Task<TResponse> DeserializeResponse<TResponse>(HttpResponseMessage response)
        {
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(responseStream);

            return JsonConvert.DeserializeObject<TResponse>(await streamReader.ReadToEndAsync());
        }
    }
}