using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TestingWebApi.Core;
using TestingWebApi.Core.Models;

namespace TestingWebApi.NUnitIntegrationTests;

[TestFixture]
public class PizzasIntegrationTests
{
    private WebApplicationFactory<Startup> _factory = null!;
    private HttpClient _httpClient = null!;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Startup>();
        _httpClient = _factory.CreateClient();
    }

    [Test]
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
        CollectionAssert.IsNotEmpty(pizzas);
    }

    [Test]
    public async Task Get_WithIdExisting_200WithOnePizza()
    {
        // Given
        HttpMethod httpMethod = HttpMethod.Get;
        const int pizzaId = 6;
        var requestUri = $"/pizzas/{pizzaId}";
        var request = new HttpRequestMessage(httpMethod, requestUri);

        var expectedPizza = new Pizza(pizzaId, "Seven Cheese", true);

        // When
        HttpResponseMessage response = await _httpClient.SendAsync(request);

        // Then
        response.EnsureSuccessStatusCode();
        var pizza = await response.Content.ReadFromJsonAsync<Pizza>();
        Assert.That(pizza, Is.EqualTo(expectedPizza));
    }

    [Test]
    public async Task Get_WithIdNotExisting_404()
    {
        // Given
        HttpMethod httpMethod = HttpMethod.Get;
        const string requestUri = "/pizzas/9999";
        var request = new HttpRequestMessage(httpMethod, requestUri);

        // When
        HttpResponseMessage response = await _httpClient.SendAsync(request);

        // Then
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task DemonstrateAssertionError()
    {
        // Given
        const int pizzaId = 6;
        var request = new HttpRequestMessage(HttpMethod.Get, $"/pizzas/{pizzaId}");

        var expectedPizza = new Pizza(pizzaId, "One hundred Cheese");

        // When
        HttpResponseMessage response = await _httpClient.SendAsync(request);

        // Then
        var pizza = await response.Content.ReadFromJsonAsync<Pizza>();
        Assert.That(pizza, Is.EqualTo(expectedPizza));
    }

    [TearDown]
    public void TearDown()
    {
        _httpClient.Dispose();
        _factory.Dispose();
    }
}