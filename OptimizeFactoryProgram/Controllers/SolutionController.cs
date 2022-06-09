using Microsoft.AspNetCore.Mvc;
using OptimizeFactoryProgram.Context;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace OptimizeFactoryProgram.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolutionController : ControllerBase
    {

        [HttpGet]
        public ActionResult<Dictionary<string, double>> Get()
        {
            using var db = new DBContext();
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "py";
            var materials = string.Join(';', db.PricesMaterial.Include(x => x.PriceOf).Select(price => $"{price.PriceOf.Id},{price.Cost},{db.MaterialAvailable.First(x => x.Material == price.PriceOf).Count}"));
            var products = string.Join(';', db.PricesProduct.OrderByDescending(x => x.ActualDate).Include(x => x.PriceOf).Select(price => $"{price.PriceOf.Id},{price.Cost}"));
            var ingridients = string.Join(';', db.Products.Include(x => x.Ingridients).Select(product => $"{product.Id},{string.Join('|', product.Ingridients.Select(x => $"{x.MaterialId}_{x.Count}"))}"));
            start.Arguments = string.Format("{0} \"{1}\" \"{2}\" \"{3}\"", "Python\\code.py", materials, products, ingridients);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string output = reader.ReadToEnd();
                    var result = output.Trim().Split('\n');
                    var optimalString = result.First().Trim();
                    optimalString = optimalString.Remove(optimalString.Length - 1).Remove(0, 1);
                    var optimal = optimalString.Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToDouble(x)).ToArray();

                    var namesString = result.Last().Trim();
                    namesString = namesString.Remove(namesString.Length - 2).Remove(0, 2);
                    var names = namesString.Split("', '", StringSplitOptions.RemoveEmptyEntries).Select(x => Guid.Parse(x)).ToArray();

                    Dictionary<string, double> res = new();
                    for (int i = 0; i < optimal.Length; ++i)
                    {
                        var name = db.Products.First(x => x.Id == names[i]);
                        res.Add(name.Name, optimal[i]);
                    }
                    return res;
                }
            }
        }

    }
}