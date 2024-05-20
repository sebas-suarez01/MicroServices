namespace Contracts;

public record GetBookAvailabilityRequest(Guid BookId, int Amount);
public record GetBookAvailabilityResponse(bool IsAvailable, string Name, decimal Price);