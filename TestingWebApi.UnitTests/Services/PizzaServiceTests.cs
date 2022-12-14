using Moq;
using TestingWebApi.Core.Models;
using TestingWebApi.Core.repositories;
using TestingWebApi.Core.Services;

namespace TestingWebApi.UnitTests.Services;

[TestFixture]
public class PizzaServiceTests
{
    private PizzaService _pizzaService;
    private Mock<IPizzaRepository> _pizzaRepositoryMock;

    [SetUp]
    public void OneTimeSetUp()
    {
        _pizzaRepositoryMock = new Mock<IPizzaRepository>();
        _pizzaService = new PizzaService(_pizzaRepositoryMock.Object);
    }

    [Test]
    public void GetAll_BasicCase_AllPizzas()
    {
        // Given
        var pizzas = new List<Pizza>
        {
            new(1, "Pizza 1", IsGlutenFree: false),
            new(2, "Pizza 2", IsGlutenFree: true),
            new(3, "Pizza 3", IsGlutenFree: false)
        };

        _pizzaRepositoryMock.Setup(x => x.Find()).Returns(pizzas);

        // When
        List<Pizza> result = _pizzaService.GetAll();

        // Then
        Assert.That(pizzas, Is.EqualTo(result).AsCollection);

        _pizzaRepositoryMock.Verify(x => x.Find(), Times.Once);
    }

    [Test]
    public void Get_ExistingId_Pizza()
    {
        // Given
        var pizza = new Pizza(1, "Pizza 1", IsGlutenFree: false);

        _pizzaRepositoryMock.Setup(x => x.Find(1)).Returns(pizza);

        // When
        Pizza? result = _pizzaService.Get(1);

        // Then
        Assert.That(pizza, Is.EqualTo(result));

        _pizzaRepositoryMock.Verify(x => x.Find(1), Times.Once);
    }

    [Test]
    public void Get_NonExistingId_Null()
    {
        // Given
        const int pizzaId = 1;

        _pizzaRepositoryMock.Setup(x => x.Find(pizzaId)).Returns(null as Delegate);

        // When
        Pizza? result = _pizzaService.Get(pizzaId);

        // Then
        Assert.That(result, Is.Null);

        _pizzaRepositoryMock.Verify(x => x.Find(pizzaId), Times.Once);
    }
}