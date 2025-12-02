using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace MyWebApi.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    // Defines the route template: /api/products (if controller is ProductsController) 
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _products = new List<Product> {
        new Product { Id = 1, Name = "Laptop", Price = 1200.00 },
        new Product { Id = 2, Name = "Mouse", Price = 25.00 },
        new Product { Id = 3, Name = "Keyboard", Price = 75.00 }
        };

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return _products; // Returns the entire list of products as JSON
        }
        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Returns a 404 Not Found if the product isn&#39;t found
            }
            return product; // Returns the specific product as JSON
        }
        // POST: api/Products
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product
        product)
        {
            if (product == null)
            {

                return BadRequest("Product object is null"); // Returns a 400 Bad Request if the product is null.
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns a 400 Bad Request with validation errors
            }
            // Simulate assigning an ID (In a real app, this would be handled by a database)
            product.Id = _products.Count > 0 ? _products[_products.Count -
            1].Id + 1 : 1; // Simple auto-increment
            _products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new
            {
                id =
            product.Id
            }, product); // Returns a 201 Created with the new product in the response body and a Location header.
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product
        product)
        {
            if (product == null || product.Id != id)
            {
                return BadRequest("Product object is null or ID mismatch"); // Returns a 400 if the product is null or IDs don&#39;t match
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns a 400 Bad Request with validation errors
            }
            var existingProduct = _products.Find(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound(); // Returns a 404 Not Found if the product doesn&#39;t exist
            }
            // Update the existing product
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return NoContent(); // Returns a 204 No Content indicatingsuccessful update.
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Returns a 404 Not Found if the product doesn't exist
            }
            _products.Remove(product);
            return NoContent(); // Returns a 204 No Content indicating successful deletion.
        }
    }
    // A simple Product model
    public class Product
    {
        public int Id { get; set; }

        //[Required] // Requires that the Name property is populated forPOST/PUT requests.
        public string Name { get; set; }
        //[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")] // Requires that the price is a valid positive number
        public double Price { get; set; }
    }

}