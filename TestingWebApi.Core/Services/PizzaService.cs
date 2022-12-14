using TestingWebApi.Core.Models;
using TestingWebApi.Core.repositories;

namespace TestingWebApi.Core.Services;

public class PizzaService: IPizzaService
{
    private readonly IPizzaRepository _pizzaRepository;

    public PizzaService(IPizzaRepository pizzaRepository)
    {
        _pizzaRepository = pizzaRepository;
    }

    public List<Pizza> GetAll() => _pizzaRepository.Find();

    public Pizza? Get(int id) => _pizzaRepository.Find(id);
}