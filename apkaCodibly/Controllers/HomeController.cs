using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AplikacjaRestAPI.Data;
using AplikacjaRestAPI.Models;

namespace home.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private MyDBContext _dbContext;
        private ILogger _logger;
        private DbManager _dbManager;
        public HomeController(MyDBContext myDBContext, ILoggerFactory loggerFactory)
        {
            _dbContext = myDBContext;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _dbManager = new DbManager(_dbContext, _logger);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Products>> Get()
        {
            return _dbContext.Products;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Products>> Get(int id)
        {
            var listResult = _dbContext.Products.Where(x => x.Id == id).ToList();
            if (listResult == null || listResult.Count == 0) _logger.LogWarning($"No matching Id {id}");
            return listResult;
        }

        [HttpPost]
        public void Post([FromBody] Products products)
        {
            _dbManager.SaveProducts(products);
        }

        [HttpPut]
        public void Put([FromBody] Products products)
        {
            _dbManager.UpdateProducts(products);
        }
    }
}
