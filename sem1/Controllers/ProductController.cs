using Microsoft.AspNetCore.Mvc;
using sem1.Models;

namespace sem1.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ProductController : ControllerBase
    {

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
    }
}
