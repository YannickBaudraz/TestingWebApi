using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TestingWebApi.Core;
using TestingWebApi.Core.Models;

namespace TestingWebApi.XUnitIntegrationTests;

[Collection("IntegrationTests")]
public sealed class PizzasIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
{
    private readonly HttpClient _httpClient;

    public PizzasIntegrationTests(WebApplicationFactory<Startup> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
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
        Assert.NotNull(pizzas);
        Assert.NotEmpty(pizzas);
    }

    [Fact]
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
        Assert.NotNull(pizza);
        Assert.Equal(expectedPizza, pizza);
    }

    [Fact]
    public async Task Get_WithIdNotExisting_404()
    {
        // Given
        HttpMethod httpMethod = HttpMethod.Get;
        const int pizzaId = 9999;
        var requestUri = $"/pizzas/{pizzaId}";
        var request = new HttpRequestMessage(httpMethod, requestUri);

        // When
        HttpResponseMessage response = await _httpClient.SendAsync(request);

        // Then
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
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
        Assert.Equal(expectedPizza, pizza);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}