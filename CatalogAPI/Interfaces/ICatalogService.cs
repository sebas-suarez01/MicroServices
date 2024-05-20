using CatalogAPI.Entities;

namespace CatalogAPI.Interfaces;

public interface ICatalogService
{
    Task<List<BookModel>> GetAll();
}