using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizeFactoryProgram.Context;

namespace OptimizeFactoryProgram.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            using var db = new DBContext();
            return db.Products
                .Include(x => x.Ingridients)
                .ThenInclude(x => x.Material)
                .ToArray();
        }
        [HttpPost]
        public ActionResult<Guid> Post(Product product)
        {
            if (ModelState.IsValid)
            {
                using var db = new DBContext();
                var savedProduct = db.Products.Add(product);
                db.SaveChanges();
                return savedProduct.Entity.Id;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}