using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RedisCaching.Data;
using RedisCaching.Dtos;
using RedisCaching.Models;
using System.Text.Json;

namespace RedisCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IDistributedCache _cache;

        public ProductsController(AppDbContext context, IDistributedCache cache)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "Products:All";
            List<Product> products;

            try
            {
                var cachedData = await _cache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData)) 
                {
                    products = JsonSerializer.Deserialize<List<Product>>(cachedData) ?? new List<Product>();
                }
                else
                {
                    products = await _context.Products.ToListAsync();
                    if (products != null)
                    {
                        // Cache the data
                        var serializedData = JsonSerializer.Serialize(products);
                        var cacheOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                        await _cache.SetStringAsync(cacheKey, serializedData, cacheOptions);
                    }
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving products.", detail = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var cacheKey = $"Product_{id}";
            Product? product;
            try
            {
                var cachedData = await _cache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    product = JsonSerializer.Deserialize<Product>(cachedData) ?? new Product();
                }
                else
                {
                    product = await _context.Products.FindAsync(id);
                    if (product == null)
                        return NotFound($"Product with ID {id} not found.");
                    // Cache the product data
                    var serializedData = JsonSerializer.Serialize(product);
                    await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    });
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the product.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] AddProduct updatedProduct)
        {
            try
            {
                var existingProduct = await _context.Products.FindAsync(id);
                if (existingProduct == null)
                    return NotFound($"Product with ID {id} not found.");

                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Category = updatedProduct.Category;
                existingProduct.Quantity = updatedProduct.Quantity;
                await _context.SaveChangesAsync();
                // Update product in cache
                var cacheKey = $"Product_{id}";
                var serializedData = JsonSerializer.Serialize(updatedProduct);
                await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the product.", details = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    return NotFound($"Product with ID {id} not found.");
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                // Remove product from cache
                var cacheKey = $"Product_{id}";
                await _cache.RemoveAsync(cacheKey);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the product.", details = ex.Message });
            }
        }


        [HttpGet("category")]
        public async Task<IActionResult> GetProductByCategory(string category)
        {
            var cacheKey = $"Products:Category:{category}";
            List<Product> products;

            try
            {
                var cachedData = await _cache.GetStringAsync(cacheKey);
                if (cachedData != null)
                {
                    products = JsonSerializer.Deserialize<List<Product>>(cachedData) ?? new List<Product>();
                }
                else
                {
                    products = await _context.Products
                        .Where(c => c.Category.ToLower() == category.ToLower())
                        .AsNoTracking()
                        .ToListAsync();

                    if (products != null && products.Count > 0)
                    {
                        var serializedData = JsonSerializer.Serialize(products);
                        var cacheOptions = new DistributedCacheEntryOptions()
                            // Resets the expiration timer each time the cache entry is accessed
                            .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                            //  Specifies an absolute time after which the cache entry expires, regardless of whether it has been accessed or not.
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                        await _cache.SetStringAsync(cacheKey, serializedData, cacheOptions);
                    }
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving products by category.", detail = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] AddProduct payload)
        {
            try
            {
                var product = new Product()
                {
                    Id = new Guid(),
                    Name = payload.Name,
                    Category = payload.Category,
                    Price = payload.Price,
                    Quantity = payload.Quantity,
                };
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                // Invalidate the cache
                await _cache.RemoveAsync("Products:All");
                await _cache.RemoveAsync($"Products:Category:{product.Category}");

                return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    message = "An error occurred while creating the product.",
                    detail = new
                    {
                        ex.Message,
                        ex.StackTrace,
                        InnerExceptionMessage = ex.InnerException?.Message,
                        InnerExceptionStackTrace = ex.InnerException?.StackTrace
                    }
                };

                return StatusCode(500, errorResponse);
            }
        }


    }
}
