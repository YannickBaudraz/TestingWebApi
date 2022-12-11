using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TestingWebApi.Core;
using TestingWebApi.Core.Models;

namespace TestingWebApi.IntegrationTests;

[TestClass]
public class PizzasIntegrationTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _httpClient = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _factory = new WebApplicationFactory<Program>();
        _httpClient = _factory.CreateClient();
    }

    [TestMethod]
    public async Task Get_BasicCase_200WithAllPizzas()
    {
        // Given
        HttpMethod httpMethod = HttpMethod.Get;
        const string requestUri = "/pizzas";
        var request = new HttpRequestMessage(httpMethod, requestUri);

        // When
        HttpResponseMessage response = await _httpClient.SendAsync(request);

        // Then
        response.EnsureSuccessStatusCode();
        var pizzas = await response.Content.ReadFromJsonAsync<List<Pizza>>();
        Assert.IsTrue(pizzas!.Any());
    }

    [TestMethod]
    public async Task Get_WithIdExisting_200WithOnePizza()
    {
        // Given
        HttpMethod httpMethod = HttpMethod.Get;
        const int pizzaId = 6;
        var requestUri = $"/pizzas/{pizzaId}";
        var request = new HttpRequestMessage(httpMethod, requestUri);

        var expectedPizza = new Pizza(6, "Seven Cheese", true);

        // When
        HttpResponseMessage response = await _httpClient.SendAsync(request);

        // Then
        response.EnsureSuccessStatusCode();
        var pizza = await response.Content.ReadFromJsonAsync<Pizza>();
        Assert.AreEqual(expectedPizza, pizza);
    }

    [TestMethod]
    public async Task Get_WithIdNotExisting_404()
    {
        // Given
        HttpMethod httpMethod = HttpMethod.Get;
        const string requestUri = "/pizzas/9999";
        var request = new HttpRequestMessage(httpMethod, requestUri);

        // When
        HttpResponseMessage response = await _httpClient.SendAsync(request);

        // Then
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestCleanup]
    public void Teardown()
    {
        _httpClient.Dispose();
        _factory.Dispose();
    }
}