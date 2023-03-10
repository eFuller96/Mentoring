using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Flurl;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToDoApplication.DataStorage;

namespace TrenitaliaRealTimeAdapter.Acceptance.Tests;

public class AcceptanceTestsFixture : WebApplicationFactory<Program>
{

    public AcceptanceTestsFixture()
    {
    }


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(
            collection =>
            {
                collection.Replace(ServiceDescriptor.Singleton(typeof(IDataStorage),
                    (services) => new FileStorage(services.GetService<IFileManager>())));
            });

        builder.ConfigureAppConfiguration(
            configurationBuilder
                => configurationBuilder.AddInMemoryCollection(
                    new KeyValuePair<string,string>[]
                    {
                    }));
    }

    public async Task<HttpResponseMessage> DoRequest(Url url, HttpMethod method, HttpContent? body = null)
    {
        using var httpRequestMessage = new HttpRequestMessage(method, url);
        if (body != null)
        {
            httpRequestMessage.Content = body;
        }

        using var httpClient = CreateClient();
        return await httpClient.SendAsync(httpRequestMessage);
    }
}