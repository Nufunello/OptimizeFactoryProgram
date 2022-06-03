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
        private readonly CompiledCode compiled;
        private readonly ScriptEngine engine;
        private readonly ScriptScope scope;
        public SolutionController()
        {
            /*engine = Python.CreateEngine();
            var sp = engine.GetSearchPaths();
            //sp.Add(@"C:\Program Files (x86)\IronPython 2.7\Lib");
            //sp.Add(@"C:\Program Files (x86)\IronPython 2.7\Lib\site-packages");
            sp.Add(@"C:\Users\nafan\anaconda3\envs\py27\Lib");
            sp.Add(@"C:\Users\nafan\anaconda3\envs\py27\Lib\site-packages");

            engine.SetSearchPaths(sp);
            scope = engine.CreateScope();
            compiled = engine
                .CreateScriptSourceFromFile(@"C:\Users\nafan\source\repos\OptimizeFactoryProgram\OptimizeFactoryProgram\Python\code.py")
                .Compile();
            compiled.Execute(scope);*/
        }

        [HttpGet]
        public ActionResult<Dictionary<string, double>> Get()
        {
            using var db = new DBContext();
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Windows\py.exe";
            var materials = string.Join(';', db.PricesMaterial.Include(x => x.PriceOf).Select(price => $"{price.PriceOf.Id},{price.Cost},{db.MaterialAvailable.First(x => x.Material == price.PriceOf).Count}"));
            var products = string.Join(';', db.PricesProduct.Include(x => x.PriceOf).Select(price => $"{price.PriceOf.Id},{price.Cost}"));
            var ingridients = string.Join(';', db.Products.Include(x => x.Ingridients).Select(product => $"{product.Id},{string.Join('|', product.Ingridients.Select(x => $"{x.MaterialId}_{x.Count}"))}"));
            start.Arguments = string.Format("{0} \"{1}\" \"{2}\" \"{3}\"", @"C:\Users\nafan\source\repos\OptimizeFactoryProgram\OptimizeFactoryProgram\Python\code.py", materials, products, ingridients);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string output = reader.ReadToEnd();
                    var result = output.Trim().Split('\n');
                    var optimal = result.First().Trim().Remove(result.First().Trim().Length - 1).Remove(0, 1).Split("  ").Select(x => Convert.ToDouble(x)).ToArray();
                    var names = result.Last().Trim().Remove(result.Last().Trim().Length - 2).Remove(0, 2).Split("', '").Select(x => Guid.Parse(x)).ToArray();
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