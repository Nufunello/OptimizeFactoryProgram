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
        public ActionResult<Guid> Post(Material material)
        {
            if (ModelState.IsValid)
            {
                using var db = new DBContext();
                var savedMaterial = db.Materials.Add(material);
                db.SaveChanges();
                return savedMaterial.Entity.Id;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}