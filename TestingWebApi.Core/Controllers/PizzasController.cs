using Microsoft.AspNetCore.Mvc;
using TestingWebApi.Core.Models;
using TestingWebApi.Core.Services;

namespace TestingWebApi.Core.Controllers;

/// <summary>
/// Controller for the <see cref="Pizza"/> model.
/// </summary>
/// <seealso href="https://learn.microsoft.com/en-us/aspnet/core/web-api/advanced/conventions?view=aspnetcore-7.0">Naming conventions</seealso>
[Route("[controller]")]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
public class PizzasController : ControllerBase
{
    private readonly PizzaService _pizzaService;

    public PizzasController(PizzaService pizzaService) => _pizzaService = pizzaService;

    [HttpGet]
    public ActionResult<List<Pizza>> Get() => _pizzaService.GetAll();

    [HttpGet("{id:int}")]
    public ActionResult<Pizza> Get(int id) => _pizzaService.Get(id) is { } pizza
        ? pizza
        : NotFound();
}