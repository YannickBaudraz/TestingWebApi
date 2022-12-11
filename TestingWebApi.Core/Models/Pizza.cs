namespace TestingWebApi.Core.Models;

public record Pizza(
    int Id,
    string Name,
    bool IsGlutenFree = false
);
