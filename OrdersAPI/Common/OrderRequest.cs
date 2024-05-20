using OrdersAPI.Entities;

namespace OrdersAPI.Common;

public record OrderRequest(List<BookRequest> Books);