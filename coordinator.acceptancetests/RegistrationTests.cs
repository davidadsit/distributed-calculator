using System.Net.Http.Json;
using coordinator.Application;
using Microsoft.AspNetCore.Mvc.Testing;

namespace coordinator.acceptancetests;

public class RegistrationTests
{
    [Test]
    public async Task When_sending_valid_registration_request()
    {
        var factory = new WebApplicationFactory<JobPool>();
        var client = factory.CreateClient();
        var body = new {
            workerId = "99fd7bb3-57d1-4d27-bc54-028c20060bef",
            teamName = "Team Name",
            createJobEndpoint = "http://example.com/create",
            errorCheckEndpoint = "http://example.com/error",
        };
        var bodyContent =  JsonContent.Create(body);
        var response = await client.PostAsync("/register", bodyContent);
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<TestRegistrationResult>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Does.Not.Contain("You must provide valid URIs"));
        Assert.That(result.Result, Does.Contain(body.teamName));
        Assert.That(result.Result, Does.Contain(body.workerId));
        Assert.That(result.Result, Does.Contain(body.createJobEndpoint));
        Assert.That(result.Result, Does.Contain(body.errorCheckEndpoint));
        Assert.That(result.Result, Does.Contain("is ready to accept jobs"));
    }

    [Test]
    public async Task When_sending_registration_request_with_invalid_workerId()
    {
        var factory = new WebApplicationFactory<JobPool>();
        var client = factory.CreateClient();
        var body = new {
            workerId = "not a valid guid",
            teamName = "Team Name",
            createJobEndpoint = "http://example.com/create",
            errorCheckEndpoint = "http://example.com/error",
        };
        var bodyContent =  JsonContent.Create(body);
        var response = await client.PostAsync("/register", bodyContent);
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<TestRegistrationResult>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.EqualTo("Invalid registration request. The worker ID may not be a valid GUID."));
    }
}

public class TestRegistrationResult
{
    public string? Result { get; set; }
}