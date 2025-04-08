using Refit;

namespace Clean.Architecture.External.Services;

public interface ISampleService
{
    [Get("/users")]
    Task<IEnumerable<SampleDto>> GetAll();

    [Get("/users/{id}")]
    Task<SampleDto> GetUser(int id);

    [Post("/users")]
    Task<SampleDto> CreateUser([Body] SampleDto user);

    [Put("/users/{id}")]
    Task<SampleDto> UpdateUser(int id, [Body] SampleDto user);

    [Delete("/users/{id}")]
    Task DeleteUser(int id);
}
