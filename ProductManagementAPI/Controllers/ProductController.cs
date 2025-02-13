using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using MediatR;
using Application.Commands;
using System.Collections;
using Application.Queries;
using FluentValidation;
using Application.Validators;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Application.Responses;
using Repository.Data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;
        private IValidator<CreateProductDTO> __productCreateValidator;
        private IValidator<UpdateProductDTO> __productUpdateValidator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            ISender sender,
            IValidator<CreateProductDTO> productCreateValidator,
            IValidator<UpdateProductDTO> productUpdateValidator,
            ILogger<ProductController> logger
        )
        {
            _sender = sender;
            __productCreateValidator = productCreateValidator;
            __productUpdateValidator = productUpdateValidator;
            _logger = logger;
        }

        [Authorize(Policy = "OnlyBoss")]
        [HttpPost]
        public async Task<IActionResult> Add(CreateProductDTO productDTO)
        {
            try
            {
                Console.WriteLine("FROM ProductController.Add");
                
                var validationResult = await __productCreateValidator.ValidateAsync(productDTO);

                if (!validationResult.IsValid)
                {
                    var errorResponse = new ApiResponse<ProductResponseDTO>(400, "Validation errors", validationResult.Errors.Select(e => e.ErrorMessage).ToList());
                    return BadRequest(errorResponse);
                }

                var command = new CreateProductCommand(productDTO);
                var result = await _sender.Send(command); // Se espera la finalización de la tarea

                _logger.LogInformation("A new product has been created: {@Result}", result);
                var successResponse = new ApiResponse<ProductResponseDTO>(201, "Product created successfully", result);
                return Ok(successResponse);

            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<ProductResponseDTO>(500, "Internal server error", new List<string> { ex.Message });
                return StatusCode(500, errorResponse);
            }
        }

        [Authorize(Policy = "OnlyUser")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDTO productDTO)
        {
            try
            {
                Console.WriteLine("FROM ProductController.Update");

                var validationResult = await __productUpdateValidator.ValidateAsync(productDTO);

                if (!validationResult.IsValid)
                {
                    var errorResponse = new ApiResponse<object>(400, "Validation errors", validationResult.Errors.Select(e => e.ErrorMessage).ToList());
                    return BadRequest(errorResponse);
                }

                var command = new UpdateProductCommand(productDTO);
                var result = await _sender.Send(command);

                var successResponse = new ApiResponse<ProductResponseDTO>(200, "Product updated successfully", result);
                return Ok(successResponse);
            }
            catch (KeyNotFoundException ex)
            {
                var notFoundResponse = new ApiResponse<ProductResponseDTO>(404, "Product not found", new List<string> { ex.Message });
                return NotFound(notFoundResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<ProductResponseDTO>(500, "Internal server error", new List<string> { ex.Message });
                return StatusCode(500, errorResponse);
            }
        }

        [Authorize(Policy = "OnlyClient")]
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllProductsQuery();
                var result = await _sender.Send(query);

                _logger.LogInformation("Se obtuvieron {Count} productos", result.Count());
                var successResponse = new ApiResponse<IEnumerable<ProductResponseDTO>>(200, "OK", result);
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<ProductResponseDTO>(500, "Internal server error", new List<string> { ex.Message });
                return StatusCode(500, errorResponse);
            } 
        }

        [Authorize(Policy = "OnlyClient")]
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetProductByIdQuery(id);
                var result = await _sender.Send(query);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    var errorResponse = new ApiResponse<ProductResponseDTO>(404, "Product not found");
                    return NotFound(errorResponse);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<ProductResponseDTO>(500, "Internal server error", new List<string> { ex.Message });
                return StatusCode(500, errorResponse);
            }
        }

        [Authorize(Policy = "OnlyAdmin")]
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteProductCommand(id);
                var result = await _sender.Send(command);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                var notFoundResponse = new ApiResponse<ProductResponseDTO>(404, "Product not found", new List<string> { ex.Message });
                return NotFound(notFoundResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<ProductResponseDTO>(500, "Internal server error", new List<string> { ex.Message });
                return StatusCode(500, errorResponse);
            }
        }
    }
}
