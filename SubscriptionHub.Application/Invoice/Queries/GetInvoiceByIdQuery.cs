using MediatR;

namespace SubscriptionHub.Application.Invoice.Queries
{
    public record GetInvoiceByIdQuery : IRequest<InvoiceDto>
    {
        public Guid Id { get; init; }
    }
}
