using CatalogAPI.Database;
using CatalogAPI.Entities;
using CatalogAPI.Interfaces;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Services;

public class CatalogService : ICatalogService
{
    private readonly CatalogDbContext _context;

    public CatalogService(CatalogDbContext context)
    {
        _context = context;
    }

    public Task<List<BookModel>> GetAll()
    {
        return _context.Set<BookModel>().ToListAsync();
    }
}