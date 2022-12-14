using TestingWebApi.Core.Models;

namespace TestingWebApi.Core.Services;

public interface IPizzaService
{
    List<Pizza> GetAll();
    Pizza? Get(int id);
}