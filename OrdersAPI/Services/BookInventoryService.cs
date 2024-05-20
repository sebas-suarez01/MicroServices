using Contracts;
using MassTransit;
using OrdersAPI.Interfaces;

namespace OrdersAPI.Services;

public class BookInventoryService : IBookInventoryService
{
    private readonly IRequestClient<GetBookAvailabilityRequest> _client;
    private readonly ILogger<BookInventoryService> _logger;

    public BookInventoryService(IRequestClient<GetBookAvailabilityRequest> client, ILogger<BookInventoryService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<(bool IsAvailable, string Name, decimal Price)> BookAvailability(Guid id, int amount)
    {
        var response =
            await _client.GetResponse<GetBookAvailabilityResponse, BookNotFound>(
                new GetBookAvailabilityRequest(id, amount));
        if (response.Is(out Response<GetBookAvailabilityResponse> isAvailableResponse))
        {
            return (isAvailableResponse.Message.IsAvailable, isAvailableResponse.Message.Name, isAvailableResponse.Message.Price);
        }

        if (response.Is(out Response<BookNotFound> notFoundResponse))
        {
            _logger.LogError($"Book Id {notFoundResponse.Message.BookId.ToString()} not found");
        }

        return (false,string.Empty,0);
    }
}