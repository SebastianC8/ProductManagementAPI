using Infrastructure.Contracts;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications
{
    public class LowStockNotificationHandler : INotificationHandler<LowStockNotification>
    {
        private IEmailService __emailService;
        private IConfiguration __configuration;

        public LowStockNotificationHandler(IEmailService emailService, IConfiguration configuration)
        {
            __emailService = emailService;
            __configuration = configuration;
        }

        public async Task Handle(LowStockNotification notification, CancellationToken cancellationToken)
        {
            var subject = $"Alerta: Stock Bajo en {notification.Product.Name}";
            var body = $"El producto '{notification.Product.Name}' (ID: {notification.Product.Id}) tiene un stock crítico de: {notification.Product.Quantity} unidades. Por favor tome acción.";
            var adminEmail = __configuration["EmailSettings:AdminEmail"];

            var sendEmail = await __emailService.SendEmailAsync(adminEmail, subject, body);
        }
    }
}
