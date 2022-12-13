using TestingWebApi.Core.Models;

namespace TestingWebApi.UnitTests.Models;

[TestFixture]
public class PizzasTests
{
    private Pizza _pizza;

    public PizzasTests() => _pizza = new Pizza(0, "");

    [SetUp]
    public void Setup() => _pizza = new Pizza(0, "");

    [Test]
    public void Constructor_AllParameters_SetsProperties()
    {
        // Given
        const int id = 1;
        const string name = "Vegan";
        const bool isGlutenFree = true;

        // When
        _pizza = new Pizza(id, name, isGlutenFree);

        // Then
        Assert.That(_pizza.Id, Is.EqualTo(id));
    }

    [Test]
    public void Equals_TwoDifferentPizzasWithSameProperties_True()
    {
        // Given
        _pizza = new Pizza(1, "Vegan", true);
        var pizza2 = new Pizza(1, "Vegan", true);

        // When
        bool result = _pizza.Equals(pizza2);

        // Then
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(_pizza, Is.EqualTo(pizza2));
        });
    }
}