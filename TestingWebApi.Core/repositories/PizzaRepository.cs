using TestingWebApi.Core.Models;

namespace TestingWebApi.Core.repositories;

public class PizzaRepository
{
    private List<Pizza> Pizzas { get; }

    public PizzaRepository()
    {
        Pizzas = new List<Pizza>
        {
            new(Id: 1, Name: "Classic Italian"),
            new(Id: 2, Name: "Veggie", IsGlutenFree: true),
            new(Id: 3, Name: "Pepperoni"),
            new(Id: 4, Name: "Hawaiian"),
            new(Id: 5, Name: "Meat Lovers"),
            new(Id: 6, Name: "Seven Cheese", true)
        };
    }
    
    public List<Pizza> FindAll() => Pizzas;

    public Pizza? Find(int id) => Pizzas.FirstOrDefault(p => p.Id == id);
}