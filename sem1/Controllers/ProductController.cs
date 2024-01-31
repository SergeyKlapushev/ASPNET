using Microsoft.AspNetCore.Mvc;
using sem1.Abstraction;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using sem1.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace sem1.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productService)
        {
            _productRepository = productService;
        }


        [HttpGet("getProduct")]
        public IActionResult GetProducts()
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var product = context.Products.Select(x => new Product()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    });
                    return Ok(product);
                }
            }
            catch 
            {
                return StatusCode(500);
            }
        }
        [HttpPut("putProduct")]
        public IActionResult PutProducts([FromQuery]string name, string description, int categoryId)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Products.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Product()
                        {
                            Name = name,
                            Description = description,
                            CategoryId = categoryId
                        });
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("addProduct")]
        public IActionResult AddProduct([FromQuery] string name, string description, int categoryId)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    context.Products.Add(new Product()
                    {
                        Name = name,
                        Description = description,
                        CategoryId = categoryId
                    });
                    context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("delProduct")]
        public IActionResult DeliteProduct([FromQuery] int productId)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if(context.Products.Any(p => p.Id.Equals(productId)))
                    {
                        context.Products.Remove(context.Products.Find(productId));
                    }
                    
                    context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("export_products_csv")]
        public IActionResult ExportProductsCsv()
        {
            var products = _productRepository.GetProducts();

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(products);
                writer.Flush();
                memoryStream.Position = 0;

                return File(memoryStream, "text/csv", "products.csv");
            }
        }


        [HttpGet("cache_statistics_csv")]
        public IActionResult GetCacheStatisticsCsv()
        {
            var cacheStatistics = _productRepository.GetCacheStatistics();

            var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecord(cacheStatistics);
                writer.Flush();
                memoryStream.Position = 0;

                return File(memoryStream, "text/csv", "cache_statistics.csv");
            }




        }
    }
}
