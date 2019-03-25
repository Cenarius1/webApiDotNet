using AplikacjaRestAPI.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AplikacjaRestAPI.Data
{
    public class DbManager
    {
        private MyDBContext _dbContext;
        private ILogger _logger;
        public DbManager(MyDBContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void SaveProducts(Products products)
        {
            if(!ProductValidator.ValidateProduct(products))
            {
                _logger.LogWarning($"Invalid product {products.Id}");
                return;
            }

            var newProducts = new Products()
            {
                Name = products.Name,
                Cost = products.Cost,
                Category = products.Category
            };

            try
            {
                _dbContext.Products.Add(newProducts);
                _dbContext.SaveChanges();
                _logger.LogInformation($"New product {products.Id} has been added to DB.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot update product {ex.Message}");
            }
        }

        public void UpdateProducts(Products products)
        {
            if (!ProductValidator.ValidateProduct(products))
            {
                _logger.LogWarning($"Invalid product {products.Id}");
                return;
            }

            bool doesIdExist = _dbContext.Products.Any(x => x.Id == products.Id);
            if (!doesIdExist) _logger.LogError($"Product Id {products.Id} doesnt exist");
            else
            {
                try
                {
                    _dbContext.Products.Update(products);
                    _dbContext.SaveChanges();
                    _logger.LogInformation($"New product {products.Id} has been added to DB.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Cannot update product {ex.Message}");
                }
            }
        }
    }
}
