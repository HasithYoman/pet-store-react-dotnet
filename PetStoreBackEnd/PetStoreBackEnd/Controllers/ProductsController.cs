using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetStoreBackEnd.Context;
using PetStoreBackEnd.DTOs;
using PetStoreBackEnd.Entities;

namespace PetStoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //create
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductDTOs dto)
        {
            var newProduct = new ProductEntity()
            {
                Brand = dto.Brand,
                Title = dto.Title,

            };
           await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return Ok("Product Saved Successfully");
        }

        //create
        [HttpGet]
        public async Task<ActionResult<List<ProductEntity>>> GetAllProduct()
        {
            var products = await _context.Products.ToListAsync();

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductEntity>> GetProductById([FromRoute] long id)
        {
            var product= await _context.Products.FirstOrDefaultAsync(q => q.Id == id);

            if(product is null) {
                return NotFound("Product Not Found");


            }
            return Ok(product);
        }

        //Update
        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateProduct([FromRoute] long id, [FromBody] CreateUpdateProductDTOs dto)
        {
            var product= await _context.Products.FirstOrDefaultAsync(q => q.Id ==id);

            if(product is null)
            {
                return NotFound("Prduct not Found");

            }
            product.Title = dto.Title;
            product.Brand = dto.Brand;

            return Ok("Product Updated Successfully");

        }

        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long id)
        {
            var product= await _context.Products.FirstOrDefaultAsync(q => q.Id == id); 
            if(product is null)
            {
                return NotFound("Product is nt found");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Product Deleted Successfully");
        }


    }
}
