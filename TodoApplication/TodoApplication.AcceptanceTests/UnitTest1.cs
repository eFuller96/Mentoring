using Flurl;
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
            var result = await acceptanceTestFixture.DoRequest<ICollection<ToDoItem>>(url,HttpMethod.Get);

            Assert.That(result,Is.EqualTo(_toDoItems));
        }

        //[Test]
        //public async Task Get_from_file_storage_csv()
        //{
        //    var acceptanceTestFixture = new AcceptanceTestsFixture(); 
        //    var baseUrl = "http://localhost:7206/TodoList/";
        //    var url = new Url($"{baseUrl}{_toDoItems.First().Id}");
        //    var result = await acceptanceTestFixture.DoRequest<ToDoItem>(url, HttpMethod.Get);

        //    Assert.That(result, Is.EqualTo(_toDoItems.First()));
        //}

        //[Test]
        //public async Task Delete_from_file_storage_csv()
        //{
        //    var acceptanceTestFixture = new AcceptanceTestsFixture();
        //    var baseUrl = "http://localhost:7206/TodoList/";
        //    var url = new Url($"{baseUrl}{_toDoItems.First().Id}");
        //    var result = await acceptanceTestFixture.DoRequest<ToDoItem>(url, HttpMethod.Delete);
        //    var expected = _toDoItems.Skip(1);

        //    Assert.That(result, Is.EqualTo(_toDoItems.First()));
        //}

        //[Test]
        // todo how to post
        //public async Task Add_to_file_storage_csv()
        //{
        //    var acceptanceTestFixture = new AcceptanceTestsFixture(); // registration csv
        //    var url = new Url("http://localhost:7206/TodoList");
        //    var httpContext = HttpContent();
        //    var result = await acceptanceTestFixture.DoRequest<ICollection<ToDoItem>>(url, HttpMethod.Post, new StreamContent() );
        //}


    }
}