using TestingWebApi.Core.Models;

namespace TestingWebApi.Core.repositories;

public interface IPizzaRepository
{
    List<Pizza> Find();
    Pizza? Find(int id);
}