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
                .ToArray();
        }
        [HttpPost]
        public ActionResult<Guid> Post(Product product)
        {
            if (ModelState.IsValid)
            {
                using var db = new DBContext();
                var savedProduct = db.Products.Add(product);
                try
                {
                    db.SaveChanges();
                    return savedProduct.Entity.Id;
                }
                catch (Exception)
                {
                    return BadRequest("Invalid Material field");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPatch]
        public ActionResult<Guid> Patch(Price<Product> price)
        {
            if (ModelState.IsValid)
            {
                using var db = new DBContext();
                var added = db.PricesProduct.Add(price);
                try
                {
                    db.SaveChanges();
                    return added.Entity.Id;
                }
                catch (Exception)
                {
                    return BadRequest("Invalid Price field");
                }
            }
            else
            {
                return BadRequest("Invalid Price field");
            }
        }
    }
}