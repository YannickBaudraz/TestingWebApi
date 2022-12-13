using TestingWebApi.Core.Models;
using TestingWebApi.Core.repositories;

namespace TestingWebApi.Core.Services;

public class PizzaService
{
    private readonly PizzaRepository _pizzaRepository;

    public PizzaService(PizzaRepository pizzaRepository)
    {
        _pizzaRepository = pizzaRepository;
    }

    public List<Pizza> GetAll() => _pizzaRepository.FindAll();

    public Pizza? Get(int id) => _pizzaRepository.Find(id);
}