using AplikacjaRestAPI.Data;
using AplikacjaRestAPI.Models;
using System.Collections.Generic;
using Xunit;

namespace AplikacjaRestAPI.Test
{
    public class ProductValidatorTest
    {
        [Theory]
        [InlineData(-1, "Name", "Category1", 10)]
        [InlineData(1, "1Name", "Category2", 10)]
        [InlineData(-1, "Name", "Category100", 10)]
        [InlineData(-1, "Name", "Category3", -10)]
        public void ValidateProduct_ForInvalidParameters_False(int Id, string name, string category, decimal cost)
        {
            Products products = GetProducts(Id, name, category, cost);

            bool result = ProductValidator.ValidateProduct(products);

            Assert.False(result);
        }

        [Theory]
        [InlineData(0, "Name", "Category1", 1)]
        [InlineData(0, "N2312ame", "Category2", null)]
        [InlineData(1, "====", "Category3", 10)]
        [InlineData(1, "@@@@", "Category4", 10)]
        public void ValidateProduct_ForValidParameters_True(int Id, string name, string category, decimal cost)
        {
            Products products = GetProducts(Id, name, category, cost);

            bool result = ProductValidator.ValidateProduct(products);

            Assert.True(result);
        }

        private Products GetProducts(int Id, string name, string category, decimal? cost)
        {
            return new Products()
            {
                Id = Id,
                Name = name,
                Category = category,
                Cost = cost
            };
        }

        private List<Products> GetProducts()
        {
            var products = new List<Products>();
            products.Add(new Products()
            {
                Name = "Test One"
            });

            return products;
        }
    }
}
