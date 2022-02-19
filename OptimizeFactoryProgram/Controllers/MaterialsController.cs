using Microsoft.AspNetCore.Mvc;
using OptimizeFactoryProgram.Context;

namespace OptimizeFactoryProgram.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Material> Get()
        {
            using var db = new DBContext();
            return db.Materials.ToArray();
        }
        [HttpPost]
        public ActionResult<Material> Post(Material material)
        {
            if (ModelState.IsValid)
            {
                using var db = new DBContext();
                db.Materials.Add(material);
                db.SaveChanges();
                return new OkResult();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}