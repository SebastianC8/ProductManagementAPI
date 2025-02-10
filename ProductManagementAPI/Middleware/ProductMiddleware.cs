using Application.DTO;
using System.Text.Json;

namespace ProductManagementAPI.Middleware
{
    public class ProductMiddleware
    {

        private readonly RequestDelegate __next;

        public ProductMiddleware(RequestDelegate next)
        {
            __next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == "POST" || context.Request.Method == "PUT")
            {
                context.Request.EnableBuffering();  // Permite volver a leer el cuerpo
                var product = await context.Request.ReadFromJsonAsync<CreateProductDTO>();

                if (product?.Name == string.Empty || product?.Quantity <= 0)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new {
                        Message = "El producto debe tener un nombre válido y una cantidad mayor que cero." 
                    }));
                    return;
                }

                context.Request.Body.Position = 0;  // Restablece la posición del stream para que otros puedan leerlo
            }

            await __next(context);
        }

    }
}
