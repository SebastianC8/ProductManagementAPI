using Core.Entities;
using MediatR;

namespace Application.Notifications
{
    public record LowStockNotification(ProductCore Product) : INotification;
}
